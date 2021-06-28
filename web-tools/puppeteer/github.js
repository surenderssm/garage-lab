const puppeteer = require('puppeteer');

async function runTest() {

    const browser = await puppeteer.launch({
        headless: false
    });

    const page = await browser.newPage();
    await page.goto('http://github.com/');
    await page.screenshot({ path: 'github.png' });
    await browser.close();
}
runTest().then(() => console.log('complete'));