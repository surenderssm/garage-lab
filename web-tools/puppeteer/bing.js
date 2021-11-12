const puppeteer = require('puppeteer');

async function runTest() {

    const browser = await puppeteer.launch({
        headless: false
    });

    const page = await browser.newPage();
    await page.goto('https://www.bing.com/');
    await page.waitForTimeout(5000);
    await page.screenshot({ path: 'bing.png' });
    
    await browser.close();
}
runTest().then(() => console.log('complete'));