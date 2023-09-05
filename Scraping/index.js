import puppeteer from 'puppeteer';

const url = "https://oibs2.metu.edu.tr/View_Program_Course_Details_64/";



const main = async () => {
    const browser = await puppeteer.launch({ headless: false });
    const page = await browser.newPage();
    await page.goto(url);

    const [el] = await page.$x('//*[@id="ContentPlaceHolder1_lblCourseTitle"]');
    const txt = await el.getProperty('textContent');
    const rawTxt = await txt.jsonValue();

    console.log({ rawTxt });

    browser.close();
}


main();