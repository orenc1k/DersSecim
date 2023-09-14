import puppeteer from "puppeteer";
import axios from "axios";
import https from "https";
import { AddCourse,GetSubjects } from "./Crud.js";

const url = "https://oibs2.metu.edu.tr/View_Program_Course_Details_64/";

const main = async () => {
  try {
    const browser = await puppeteer.launch({ headless: false });
    const page = await browser.newPage();
    await page.goto(url);
    await page.waitForSelector('select[name="select_dept"]');
    await new Promise((resolve) => setTimeout(resolve, 2000));
    const instance = axios.create({
      httpsAgent: new https.Agent({ rejectUnauthorized: false }),
    });
    var successfulResponses = 0;
    const { optionValues, departments } = await page.evaluate(
      (optionValues, departments) => {
        const selectElement = document.querySelector(
          'select[name="select_dept"]'
        );
        if (!selectElement) {
          return { optionValues: [], departments: [] };
        }
        const options = selectElement.querySelectorAll("option");

        const filteredOptions = Array.from(options).filter(
          (option) =>
            option.textContent &&
            !option.textContent.includes("Kuzey Kıbrıs Kampüsü")
        );

        const optionValueArray = [];
        const departmentsArray = [];

        filteredOptions.forEach((option) => {
          const value = option.value.trim();
          if (value !== "") {
            const new_Department = {
              deptCode: parseInt(value),
              deptShortName: "",
              deptFullName: option.textContent
                ? option.textContent.trim().split("/")[0]
                : "",
            };
            optionValueArray.push(value);
            departmentsArray.push(new_Department);
          }
        });

        return {
          optionValues: optionValueArray,
          departments: departmentsArray,
        };
      }
    );
    await new Promise((resolve) => setTimeout(resolve, 1000));
    debugger;

    await page.select('select[name="select_dept"]', "572");
    await page.select('select[name="select_semester"]', "20221");
    debugger;

    await page.click('input[type="submit"][name="submit_CourseList"]');
    await new Promise((resolve) => setTimeout(resolve, 2000));
    

    
    
      
    const CourseData = await page.evaluate(() => {
      const courseData = [];
      const tableRows = document.querySelectorAll("table tbody tr");
      const promises = Array.from(tableRows).map(async (row) => {
        const columns = row.querySelectorAll("td");
        if (columns.length > 5) {
          var courseCode = columns[1].textContent.trim();
          if (courseCode == "Code")
            return ;
            const ectsCredit = parseFloat(columns[3].textContent.trim());
            const testValue = "1.5";
            const parsedValue = parseFloat(testValue);
            const course = {
              subjectCode: parseInt(courseCode),
              subjectName: columns[2].textContent.split("(")[0].trim(),
              ectsCredit: parseFloat(ectsCredit),
              subjectCredit: parseFloat(
                columns[4].textContent.split("(")[0].trim()
              ),
              subjectLevel: columns[5].textContent.split("/")[0].trim(),
              subjectType: columns[6].textContent.split("/")[0].trim(),
            };
            if (courseCode == "5720101") {
              debugger;
            
            } else {
              console.log("course:", course);

            }
            console.log("course:", course);
            courseData.push(course);
            await Promise.all(promises);
     }
    
    });
    
    return courseData;
  });


console.log("data:",CourseData);

  for(let course in CourseData){
    instance.post('https://localhost:7031/api/Subjects/AddSubject', course).then((response) => {
      console.log('POST request successful');       // CREATE DEPARTMENT
      console.log(response.course); 
       successfulResponses++;
    }
    ).catch((error) => {
      console.error('POST request failed:', error);
    }
    );
  }

await new Promise((resolve) => setTimeout(resolve, 2000000));

/*     const data = await page.evaluate(() => {
      const tableRows = document.querySelectorAll("table tbody tr");
      console.log("tableRows:", tableRows);
      const courseData = [];
      tableRows.forEach(async (row) => {
        const columns = row.querySelectorAll("td");
        if (columns.length > 5) {
          var courseCode = columns[1].textContent.trim();
          if (courseCode == "Code");
          else {
            const ectsCredit = columns[3].textContent.trim();
            const course = {
              subjectCode: parseInt(courseCode),
              subjectName: columns[2].textContent.split("(")[0].trim(),
              ectsCredit: parseFloat(columns[3].textContent.trim()),
              subjectCredit: parseFloat(
                columns[4].textContent.split("(")[0].trim()
              ),
              subjectLevel: columns[5].textContent.split("/")[0].trim(),
              subjectType: columns[6].textContent.split("/")[0].trim(),
            };
            if (courseCode == "5720101") {
              
              debugger;
              await GetSubjects();
              console.log("course:", course);
            } else console.log("course:", course);
          }
        }
      });

      return courseData;
    }); */
    const fulldepts = [];
    /*       for (let dep in departments) {
        fulldepts.push(departments[dep].deptFullName);
        const addedDepartment = {
          deptCode: departments[dep].deptCode,
          deptShortName: departments[dep].deptShortName,
          deptFullName: departments[dep].deptFullName,
        };

         console.log(addedDepartment);
         instance.post('https://localhost:7031/api/Department/AddDepartment', addedDepartment)
         .then((response) => {
           console.log('POST request successful');       // CREATE DEPARTMENT
           console.log(response.addedDepartment); 
            successfulResponses++;
         })
         .catch((error) => {
           console.error('POST request failed:', error);
         });
      }   */

    /*     console.log(optionValues);
    console.log(successfulResponses);
    await new Promise((resolve) => setTimeout(resolve, 5000));

    for (let option of optionValues) {
      await page.select('select[name="select_dept"]', await option.toString());
      await page.select('select[name="select_semester"]', "20221");
      await page.click('input[type="submit"][name="submit_CourseList"]');
      console.log(await option.toString());
      console.log("test");
      await page.waitForSelector("#formmessage", { timeout: 5000 });
      const messageElement = await page.$("#formmessage");
      if (messageElement) {
        console.log("test2");
        const messageText = await page.evaluate((element) => {
          return element.textContent;
        }, messageElement);
        
        console.log(`Message: ${messageText}`);
        if ( messageText.includes("Information about the department could not be found.")) 
        { console.log("test3");
          await page.select('select[name="select_semester"]', "20221");
        } else {
          const data = await page.evaluate(() => {
            const tableRows = document.querySelectorAll('table tbody tr');
            console.log("tableRows:",tableRows);
            const courseData = [];
            tableRows.forEach(async(row) => {
              const columns = row.querySelectorAll('td');
              if (columns.length>5){
                var courseCode = columns[1].textContent.trim();
                if (courseCode=="Code") ;
                else {
                   const course = {
                    subjectCode: parseInt(courseCode),
                    subjectName: columns[2].textContent.split("(")[0].trim(),
                    ectsCredit: parseFloat(columns[3].textContent.trim()),
                    subjectCredit: parseFloat(columns[4].textContent.split("(")[0].trim()),
                    subjectLevel: columns[5].textContent.split("/")[0].trim(),
                    subjectType: columns[6].textContent.split("/")[0].trim(),
                  }
                  await AddCourse(course);
                  console.log("course:",course);
                }
              }
            });
        
            return courseData;
          });
          await new Promise((resolve) => setTimeout(resolve, 1000));
          await page.waitForSelector('input[type="submit"][name="SubmitBack"]');
          await page.click("input[type=submit][name=SubmitBack]");
          await new Promise((resolve) => setTimeout(resolve, 1000));
        }
      }
    } */

    await new Promise((resolve) => setTimeout(resolve, 2000000));

    console.log(data);

    // Print or process the extracted data as needed
    console.log(data);
    /*     await page.waitForSelector('input[type="submit"][name="SubmitBack"]');
    await page.click("input[type=submit][name=SubmitBack]");
    await new Promise((resolve) => setTimeout(resolve, 2000));
    await page.select('select[name="select_dept"]', "571");
    await page.select('select[name="select_semester"]', "20221");
    await page.click('input[type="submit"][name="submit_CourseList"]');
    await new Promise((resolve) => setTimeout(resolve, 5000)); */
    await browser.close();
  } catch (error) {
    console.error("An error occurred:", error);
  }
};

main();
