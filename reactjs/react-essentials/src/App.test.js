import { render } from "@testing-library/react"
import React from "react"
import App from "./App"

test("renders an h1", () => {
    const { getByText } = render(<App />);
    // regular expression
    const h1 = getByText(/Hello for test/);

    expect(h1).toHaveTextContent("Hello for test");
});