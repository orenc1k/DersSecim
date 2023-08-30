/* const axios = require('axios');
const cheerio = require('cheerio');
const puppeteer = require("puppeteer");

const url = "https://oibs2.metu.edu.tr/View_Program_Course_Details_64/";



interface Course {
    code: string;
    name: string;
    ects: string;
    credit: string;
    level: string;
    type: string;
  }

const main = async () => {
  const browser = await puppeteer.launch({ headless: false });

  const page = await browser.newPage();
  let dersSayisi = 0;

  try {
    await page.goto(url);
    await page.waitForSelector('select[name="select_dept"]');
    await page.select('select[name="select_dept"]', "571");
    await page.click('input[type="submit"][name="submit_CourseList"]');
    await new Promise((resolve) => setTimeout(resolve, 1000));

    await page.waitForSelector(
        'input[type="radio"][name="text_course_code"]'
      );
      await page.click(
        'input[type="radio"][name="text_course_code"][value="' +
          5710111 +
          '"]'
      );
      await new Promise((resolve) => setTimeout(resolve, 1000));



            let rows;
            const courseData = await page.evaluate(() => {
                const courses: Course[] = [];
              
                // Select all rows within the table
                 rows = document.querySelectorAll('tr');
            
              
                return courses;
              });

                console.log(courseData);
                console.log (rows);


        await new Promise((resolve) => setTimeout(resolve, 4000));


    
} catch (error) {
    console.error("An error occurred:", error);
  } finally {
    await browser.close();
  }

  console.log("Done.");
  console.log(dersSayisi);
};

main(); */

interface Department {
  deptCode: number;
  deptShortName: string;
  deptFullName: string;
}

interface Course {
  code: string;
  name: string;
  ects: string;
  credit: string;
  level: string;
  type: string;
}
type EvaluationResult = {
  optionValues: string[];
  departments: Department[];
};

const puppeteer = require("puppeteer");

const url = "https://oibs2.metu.edu.tr/View_Program_Course_Details_64/";

const main = async () => {
  const browser = await puppeteer.launch({ headless: false });

  const page = await browser.newPage();
  let dersSayisi = 0;
  let text = "";
  try {
    await page.goto(url);
    await page.waitForSelector('select[name="select_dept"]');
    await new Promise((resolve) => setTimeout(resolve, 1000));

    const { optionValues, departments } = await page.evaluate(
      (optionValues: string[], departments: Department[]): EvaluationResult => {
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

        const optionValueArray: string[] = [];
        const departmentsArray: Department[] = [];

        filteredOptions.forEach((option) => {
          const value = option.value.trim();
          if (value !== "") {
            const new_Department: Department = {
              deptCode: parseInt(value),
              deptShortName: "",
              deptFullName: option.textContent
                ? option.textContent.trim().split("/")[0]
                : "",
            };
            optionValueArray.push(value);
 /*            console.log(new_Department);
            console.log(departmentsArray); */
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

    const fulldepts : string [] = [];
/*     console.log(optionValues);
    console.log(departments); */
    for (let dep in departments) {
      fulldepts.push(departments[dep].deptFullName);
    }
    console.log(fulldepts);
    await new Promise((resolve) => setTimeout(resolve, 1000));

    await page.select('select[name="select_dept"]', "572");
    await page.click('input[type="submit"][name="submit_CourseList"]');
    await new Promise((resolve) => setTimeout(resolve, 2000));

    const allCourses : string [] = [];
    const scrapedData = await page.evaluate(() => {
      const courseData: Course[] = [];
      const rows = document.querySelectorAll('table tr');
    
      for (let i = 2; i < rows.length; i++) { // Skip the header row
        const columns = rows[i].querySelectorAll('td');
        if (columns.length >= 7) { // Ensure the row has enough columns
          const courseCode = columns[1].textContent ? columns[1].textContent.trim() : '';
          const courseName = columns[2].textContent ? columns[2].textContent.trim().split("(")[0].trim() : '';
          const ectsCredit = columns[3].textContent ? columns[3].textContent.trim() : '';
          const credit = columns[4].textContent ? columns[4].textContent.trim() : '';
          const level = columns[5].textContent ? columns[5].textContent.trim() : '';
          const courseType = columns[6].textContent ? columns[6].textContent.trim() : '';
    
          const newCourse = {
            'code': courseCode,
            'name': courseName,
            'ects': ectsCredit,
            'credit': credit,
            'level': level,
            'type': courseType
          };
          if (courseCode !== 'Code')
          courseData.push(newCourse);
        }
      }
    
      return courseData;
    });
    for (let courses in scrapedData) {
      allCourses.push(scrapedData[courses].name);
    }
    console.log(scrapedData);
    console.log(allCourses);

/*     const fs = require('fs');
    
    // Convert the array to a JSON string
    const arrayAsJSON = JSON.stringify(allCourses, null, 2); // Use 2 spaces for formatting
    
    // Specify the file path where you want to save the text file
    const filePath = 'AEE.txt'; // Change this to your desired file path
    
    // Write the array as JSON to the text file
    fs.writeFile(filePath, arrayAsJSON, (err:string) => {
      if (err) {
        console.error('Error writing to file:', err);
      } else {
        console.log('Array has been written to the file successfully.');
      }
    }); */

    /*     for (let option of optionValues) {
      await page.select('select[name="select_dept"]', option.toString());
      await page.click('input[type="submit"][name="submit_CourseList"]');
      await new Promise((resolve) => setTimeout(resolve, 1000));

      const messageElement = await page.$("#formmessage");
      if (messageElement) {
        const messageText = await page.evaluate(
          (element: HTMLElement) => element.textContent,
          messageElement
        );
        console.log(`Message: ${messageText}`);
        if (
          messageText.includes(
            "Information about the department could not be found."
          )
        ) {
        } else {
          let dersBaslangicSayisi = option * 10000;
          let dersBitisSayisi = dersBaslangicSayisi + 999;
          for (
            dersBaslangicSayisi;
            dersBaslangicSayisi <= dersBitisSayisi;
            dersBaslangicSayisi++
          ) {
            try {
              await page.waitForSelector(
                'input[type="radio"][name="text_course_code"]'
              );
              await page.click(
                'input[type="radio"][name="text_course_code"][value="' +
                  dersBaslangicSayisi +
                  '"]'
              );
              await page.waitForSelector(
                'input[type="submit"][name="SubmitCourseInfo"]'
              );
              await page.click('input[type="submit"][name="SubmitCourseInfo"]');
              await new Promise((resolve) => setTimeout(resolve, 1000));

              for (let value = 1; value <= 200; value++) {
                try {
                  await page.click(
                    'input[type="submit"][name="submit_section"][value="' +
                      value +
                      '"]'
                  );
                  await new Promise((resolve) => setTimeout(resolve, 1000));
                  const messageElement = await page.$("#formmessage");
                  if (messageElement) {
                    const messageText = await page.evaluate(
                      (element: HTMLElement) => element.textContent,
                      messageElement
                    );
                    console.log(`Message: ${messageText}`);
                    if (
                      !messageText.includes(
                        "There is no section criteria to take the selected courses for this section."
                      )
                    ) {
                      await page.waitForSelector(
                        'input[type="submit"][name="SubmitBack"]'
                      );
                      await page.click("input[type=submit][name=SubmitBack]");
                      await new Promise((resolve) => setTimeout(resolve, 1000));
                    } else {
                      console.log("Skipping back button due to message.");
                    }
                  }
                } catch (error) {
                  console.error(`An error occurred for value ${value}:`, error);
                }
              }
              await page.waitForSelector(
                'input[type="submit"][name="SubmitBack"]'
              );
              await page.click("input[type=submit][name=SubmitBack]");
              await new Promise((resolve) => setTimeout(resolve, 1000));
              dersSayisi++;
            } catch (error) {
              console.error(
                `An error occurred for dersnumarası ${dersBaslangicSayisi}:`,
                error
              );
            }
          }
          await page.waitForSelector('input[type="submit"][name="SubmitBack"]');
          await page.click("input[type=submit][name=SubmitBack]");
          await new Promise((resolve) => setTimeout(resolve, 1000));
        }
      }
    } */
  } catch (error) {
    console.error("An error occurred:", error);
  } finally {
    await browser.close();
  }

  console.log("Done.");
  console.log(dersSayisi);
};

main();
