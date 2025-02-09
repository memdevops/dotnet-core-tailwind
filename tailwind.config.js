/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.cshtml',         // Razor views
        './Pages/**/*.cshtml',         // Razor pages
        './Shared/**/*.cshtml',        // Shared Razor components
        './wwwroot/**/*.html',         // Include HTML files from wwwroot
        './node_modules/flowbite/**/*.js', // Flowbite JS files
    ],
    safelist: [
        // Add only if necessary
        /^datatable-.*/, // Safelist all "datatable-*" classes if actually used
    ],
    theme: {
        extend: {
            fontFamily: {
                helvetica: ["HelveticaNeueLTArabic", "sans-serif"],
                sans: ["Open Sans", "sans-serif"],
                body: ["Open Sans", "sans-serif"],
                mono: [
                    "ui-monospace",
                    "SFMono-Regular",
                    "Menlo",
                    "Monaco",
                    "Consolas",
                    "Liberation Mono",
                    "Courier New",
                    "monospace"
                ]
            },
            fontWeight: {
                thin: 100,
                extralight: 200,
                light: 300,
                normal: 400,
                medium: 500,
                bold: 700,
                heavy: 800,
                black: 900,
            },
            boxShadow: {
                sm: "0 2px 9px -5pc rgb(0 0 0 / 0.15)",
                DEFAULT: "0 5px 10px 0 rgb(0 0 0 / 0.12)",
                md: "0 4px 6px -1px rgb(0 0 0 / 0.12), 0 2px 4px -1px rgb(0 0 0 / 0.07)",
                lg: "0 8px 26px -4px rgb(0 0 0 / 0.15), 0 8px 9px -5px rgb(0 0 0 / 0.06)",
                xl: "0 23px 45px -11px rgb(0 0 0 / 0.25)",
                "2xl": "0 20px 27px 0 rgb(0 0 0 / 0.05)",
                inner: "inset 0 1px 2px 0 rgb(0 0 0 / 0.075)",
                none: "0 0 #000"
            },
            transitionProperty: {
                width: "width"
            },
            minWidth: {
                20: "20rem"
            },
            colors: {
                // Add your custom colors here
            },
        },
    },
    plugins: [
        require('flowbite/plugin')({
            charts: true,
        }),
    ],
};
