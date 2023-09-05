import puppeteer from 'puppeteer';
import axios from 'axios';
import https from 'https';


const url = 'https://oibs2.metu.edu.tr/View_Program_Course_Details_64/';

const main = async () => {
  try {
    const browser = await puppeteer.launch({ headless: false });
    const page = await browser.newPage();
    await page.goto(url);
    await page.waitForSelector('select[name="select_dept"]');
    await new Promise((resolve) => setTimeout(resolve, 2000));
    const instance = axios.create({
        httpsAgent: new https.Agent({ rejectUnauthorized: false })});

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

      const fulldepts = [];
      /*     console.log(optionValues);
      console.log(departments); */
      for (let dep in departments) {
        fulldepts.push(departments[dep].deptFullName);
        const addedDepartment = {
          deptCode: departments[dep].deptCode,
          deptShortName: departments[dep].deptShortName,
          deptFullName: departments[dep].deptFullName,
        };

         console.log(addedDepartment);
         instance.post('https://localhost:7031/api/Department/AddDepartment', addedDepartment)
         .then((response) => {
           // Handle the response
           console.log('POST request successful');
           console.log(response.addedDepartment); // Log the response data
         })
         .catch((error) => {
           // Handle errors
           console.error('POST request failed:', error);
         });
      }  
          
    await browser.close();
  } catch (error) {
    console.error('An error occurred:', error);
  }
};

main();
