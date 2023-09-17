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

    let unsuccessfulResponses = 0;



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
        console.log(optionValues);
    await new Promise((resolve) => setTimeout(resolve, 2000));
    const Years= ["20231"];
    for (let year in Years) {
    for (let option of optionValues) {
      await page.select('select[name="select_dept"]', await option.toString());
      await page.select('select[name="select_semester"]', Years[year]);
      await page.click('input[type="submit"][name="submit_CourseList"]');

      await page.waitForSelector("#formmessage", { timeout: 5000 });
      const messageElement = await page.$("#formmessage");
      if (messageElement) {
        const messageText = await page.evaluate((element) => {
          return element.textContent;
        }, messageElement);
        
        if ( messageText.includes("Information about the department could not be found.")) 
        { 
          await page.select('select[name="select_semester"]', Years[year]);
        } else {
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
                  };
                  courseData.push(course);
           }
          
          });
          
          return courseData;
        });
      
            
        for(let course in CourseData){
/*           const addedCourse = {
            subjectCode: CourseData[course].subjectCode,
            subjectName: CourseData[course].subjectName,
            ectsCredit: CourseData[course].ectsCredit,
            subjectCredit: CourseData[course].subjectCredit,
            subjectLevel: CourseData[course].subjectLevel,
            subjectType: CourseData[course].subjectType,
          }
          await instance.post('https://localhost:7031/api/Subjects/AddSubject', addedCourse).then((response) => {
             successfulResponses++;
          }  */

          const addedAvailableCourse = {
            subjectCode: CourseData[course].subjectCode
          }
          await instance.post('https://localhost:7031/api/AvailableCourses/AddAvailableCourse',addedAvailableCourse).then((response) => { 
            successfulResponses++;
          }
          ).catch((error) => {
            console.error('POST request failed:', error);
            unsuccessfulResponses++;
          }
          );
        }
        console.log("CourseData:", CourseData);
          await page.waitForSelector('input[type="submit"][name="SubmitBack"]');
          await new Promise((resolve) => setTimeout(resolve, 500));
          await page.click("input[type=submit][name=SubmitBack]");
          await new Promise((resolve) => setTimeout(resolve, 1000));
        }
      }
      console.log("department:", option);
    } 
  }

  console.log("unsuccessfulResponses:", unsuccessfulResponses);

    // Print or process the extracted data as needed
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
