document.addEventListener("DOMContentLoaded", function () {
    const dropdownMenu = document.getElementById("language-dropdown");
    const languageLinks = dropdownMenu.querySelectorAll("a");
    const languageButtonUse = document.querySelector("[data-dropdown-toggle='language-dropdown'] svg use");

    const flagIcons = {
        en: "/images/icons.svg#flag-us", // English Flag
        ar: "/images/icons.svg#flag-om"  // Arabic Flag
    };

    // Add event listener for each language link
    languageLinks.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();
            const selectedLanguage = this.getAttribute("data-lang");
            switchLanguage(selectedLanguage, languageButtonUse, flagIcons);
        });
    });

    // Apply language settings on page load based on localStorage
    const savedLanguage = localStorage.getItem("language") || "en";
    switchLanguage(savedLanguage, languageButtonUse, flagIcons);
});

// Switch language function with refactored code
function switchLanguage(lang, languageButtonUse, flagIcons) {
    const htmlElement = document.documentElement;
    const stylesheet = document.getElementById("language-stylesheet");
    const basePath = "/css"; // Base path for the stylesheets
    const langFile = `${basePath}/styles-${lang}.css`;

    // Update direction, lang attribute, and flag icon
    htmlElement.setAttribute("dir", lang === "ar" ? "rtl" : "ltr");
    htmlElement.setAttribute("lang", lang);
    localStorage.setItem("language", lang);

    // Update the stylesheet
    updateStylesheet(stylesheet, langFile);

    // Update the flag icon
    if (languageButtonUse) {
        languageButtonUse.setAttribute("href", flagIcons[lang]);
    }

    // Load and update text content from the corresponding language JSON file
    setTimeout(() => loadLanguageFile(lang), 100); // Delay ensures proper update
}

// Update stylesheet function to avoid repetitive code
function updateStylesheet(stylesheet, langFile) {
    if (stylesheet) {
        stylesheet.href = langFile;
    } else {
        const newStylesheet = document.createElement("link");
        newStylesheet.id = "language-stylesheet";
        newStylesheet.rel = "stylesheet";
        newStylesheet.href = langFile;
        document.head.appendChild(newStylesheet);
    }
}

// Load language file (JSON) and update the text content
function loadLanguageFile(lang) {
    fetch(`/json/${lang}.json`)  // Adjust the path based on where the JSON files are stored
        .then(response => response.json())
        .then(translations => {
            // Update the text content with the loaded translations
            updateTextContent(translations);
        })
        .catch(error => console.error("Error loading language file:", error));
}

// Update text content based on loaded translations
function updateTextContent(translations) {
    document.querySelectorAll("[data-translate]").forEach(element => {
        const key = element.getAttribute("data-translate");
        if (!translations[key]) return; // Skip if translation key is missing

        if (element.tagName === "INPUT" || element.tagName === "TEXTAREA") {
            element.placeholder = ""; // Clear previous value to force update
            setTimeout(() => {
                element.placeholder = translations[key]; // Update placeholder for inputs
            }, 50);
        } else if (element.tagName === "TITLE") {
            document.title = translations[key]; // Update document title
        } else {
            element.textContent = translations[key]; // Update text for other elements
        }
    });
}