class Autocomplete {
    // Constructorul componentei
    constructor(inputId, dropdownId, sourceIdentifier = "default") {
        this.input = document.getElementById(inputId);
        this.dropdown = document.getElementById(dropdownId);
        this.sourceIdentifier = sourceIdentifier;
        this.currentFocus = -1;
        this.debounceTimer = null; // intarzierea cautarii
        this.minCharacters = 2; // incepem cautarea cand sunt introduse minim 2 caractere
        this.debounceDelay = 300; // intarziere de cautare (in ms)

        if (!this.input || !this.dropdown) {
            console.error(
                `Nu au fost gasite elementele pentru inputID: ${inputId} sau dropdownId: ${dropdownId}`
            );
            return;
        }

        this.init();
    }

    // Configuram EventListener-ii necesari
    init() {
        this.input.addEventListener("input", (e) => this.handleInput(e));
        this.input.addEventListener("keydown", (e) => this.handleKeydown(e));
        document.addEventListener("click", (e) => this.handleOutsideClick(e));
    }

    // Gestionarea introducerii de text in input
    handleInput(e) {
        const query = e.target.value.trim();

        clearTimeout(this.debounceTimer);

        if (query.length < this.minCharacters) {
            this.hideDropdown();
            return;
        }

        // intarziem cautarea pentru a evita cautari multiple
        this.debounceDelay = setTimeout(() => {
            this.searchSuggestions(query);
        }, this.debounceDelay);
    }

    // Asteptam raspunsul de la modelul paginii pentru textul introdus
    async searchSuggestions(query) {
        try {
            const response = await fetch(
                `/Index?handler=Suggestions&query=${encodeURIComponent(
                    query
                )}&source=${encodeURIComponent(this.sourceIdentifier)}`,
                {
                    method: "GET",
                    headers: {
                        "RequestVerificationToken": this.getAntiForgeryToken(),
                    },
                }
            );

            if (response.ok) {
                const suggestions = await response.json();
                this.displaySuggestions(suggestions);
            } else {
                console.error(
                    `Eroare in obitnerea de sugestii: ${response.status} ${response.statusText}`
                );
                this.hideDropdown();
            }
        } catch (error) {
            console.error("Eroare in obitnerea de sugestii:", error);
            this.hideDropdown();
        }
    }

    // Afisam rezultatele obtinute
    displaySuggestions(suggestions) {
        this.dropdown.innerHTML = "";
        this.currentFocus = -1;

        // mesajul daca nu au fost gasite sugestii
        if (!suggestions || suggestions.length === 0) {
            const noResults = document.createElement("div");
            noResults.className = "no-results";
            noResults.textContent = "Nu au fost gasite sugestii";
            this.dropdown.appendChild(noResults);
            this.showDropdown();
            return;
        }

        // construim elementele de afisarea  fiecarui rezultat
        suggestions.forEach((suggestion) => {
            const item = document.createElement("div");
            item.className = "suggestion-item";
            item.innerHTML = `
                <li class="suggestion-text">${this.highlightMatch(
                suggestion.text,
                this.input.value
            )}<br /><small style="font-size:10px">
                ${suggestion.optional1
                    ? `${suggestion.optional1}`
                    : ""
                }
                ${suggestion.optional2 
                    ? `${suggestion.optional2}`
                    : ""
                }
            </small></li>`;

            item.addEventListener("click", () => {
                this.selectSuggestion(suggestion.text);
            });
            this.dropdown.appendChild(item);
        });
        this.showDropdown();
    }

    // Evidentiem textul regasit in input
    highlightMatch(text, query) {
        if (!text || !query) return text || "";
        try {
            const regex = new RegExp(`(${this.escapeRegExp(query)})`, "gi");
            return text.replace(regex, "<strong>$1</strong>");
        } catch (e) {
            return text;
        }
    }

    // Eliminam caracterele speciale ce afecteaza cautarea regex
    escapeRegExp(string) {
        return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"); 
    }

    // Gestionam navigarea listei de la tastatura
    handleKeydown(e) {
        const items = this.dropdown.querySelectorAll(".suggestion-item");
        if (
            this.dropdown.classList.contains("hidden") &&
            e.key !== "ArrowDown" && 
            e.key !== "ArrowUp"
        ) {
            if (items.length === 0 && e.key === "Enter") {
                this.hideDropdown(); 
            }
            return;
        }

        // navigarea in jos
        if (e.key === "ArrowDown") {
            e.preventDefault();
            if (items.length === 0 && this.input.value.length >= this.minCharacters) {
                this.searchSuggestions(this.input.value.trim());
                return; 
            }
            this.currentFocus++;
            if (this.currentFocus >= items.length) this.currentFocus = 0;
            this.setActiveItem(items);
        }
        // navigarea in sus
        else if (e.key === "ArrowUp") {
            e.preventDefault();
            if (items.length === 0) return;
            this.currentFocus--;
            if (this.currentFocus < 0) this.currentFocus = items.length - 1;
            this.setActiveItem(items);
        }
        // selectarea unei sugestii
        else if (e.key === "Enter") {
            if (this.currentFocus > -1 && items[this.currentFocus]) {
                e.preventDefault(); 
                const text = items[this.currentFocus].querySelector(
                    ".suggestion-text"
                ).textContent;
                this.selectSuggestion(text); 
            } else {
                this.hideDropdown(); 
            }
        }
        // ascunderea listei
        else if (e.key === "Escape") {
            e.preventDefault();
            this.hideDropdown();
        }
    }

    // selectarea elementului activ din lista
    setActiveItem(items) {
        items.forEach((item) => item.classList.remove("active"));
        if (items[this.currentFocus]) {
            items[this.currentFocus].classList.add("active");
            items[this.currentFocus].scrollIntoView({
                behavior: "smooth",
                block: "nearest",
            });
        }
    }

    // selectarea sugestiei si actualizarea campului de cautare
    selectSuggestion(text) {
        this.input.value = text;
        this.hideDropdown();
    }

    // afisarea meniului cu sugestii
    showDropdown() {
        this.dropdown.classList.remove("hidden");
    }

    // ascunderea meniului cu sugestii
    hideDropdown() {
        this.dropdown.classList.add("hidden");
        this.currentFocus = -1;
    }

    // ascunderea la apasarea in afara componentei
    handleOutsideClick(e) {
        if (
            this.input &&
            this.dropdown &&
            !this.input.contains(e.target) &&
            !this.dropdown.contains(e.target)
        ) {
            this.hideDropdown();
        }
    }

    // obtinerea tokenului anti-falsificare pentru cererile ajax
    getAntiForgeryToken() {
        const tokenElement = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        );
        return tokenElement ? tokenElement.value : "";
    }
}

// Initializarea autocompletarii pentru pagina principala
document.addEventListener("DOMContentLoaded", () => {
    new Autocomplete("input-destinatie", "dropdown-destinatie", "destinatii");
    new Autocomplete("input-puncte-plecare", "dropdown-puncte-plecare", "puncte_plecare");
});
