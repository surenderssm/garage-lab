const puppeteer = require('puppeteer');
const lighthouse = require('lighthouse')

async function runTest() {

    const browser = await puppeteer.launch({
        headless: true,
        // slowMo: 250,
    });

    const page = await browser.newPage();
    await page.goto('http://github.com/');
    // await page.screenshot({ path: 'github.png' });
}

runTest().then(() => console.log('complete'));