class Autocomplete {
    constructor(inputId, dropdownId, sourceIdentifier = "default") {
        this.input = document.getElementById(inputId);
        this.dropdown = document.getElementById(dropdownId);
        this.sourceIdentifier = sourceIdentifier;
        this.currentFocus = -1;
        this.debounceTimer = null;
        this.minCharacters = 2;
        this.debounceDelay = 300;

        if (!this.input || !this.dropdown) {
            console.error(
                `Nu au fost gasite elementele pentru inputID: ${inputId} sau dropdownId: ${dropdownId}`
            );
            return;
        }

        this.init();
    }

    init() {
        this.input.addEventListener("input", (e) => this.handleInput(e));
        this.input.addEventListener("keydown", (e) => this.handleKeydown(e));
        document.addEventListener("click", (e) => this.handleOutsideClick(e));
    }

    handleInput(e) {
        const query = e.target.value.trim();

        clearTimeout(this.debounceTimer);

        if (query.length < this.minCharacters) {
            this.hideDropdown();
            return;
        }

        this.debounceDelay = setTimeout(() => {
            this.searchSuggestions(query);
        }, this.debounceDelay);
    }

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

    displaySuggestions(suggestions) {
        this.dropdown.innerHTML = "";
        this.currentFocus = -1;

        if (!suggestions || suggestions.length === 0) {
            const noResults = document.createElement("div");
            noResults.className = "no-results";
            noResults.textContent = "Nu au fost gasite sugestii";
            this.dropdown.appendChild(noResults);
            this.showDropdown();
            return;
        }

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

    highlightMatch(text, query) {
        if (!text || !query) return text || "";
        try {
            const regex = new RegExp(`(${this.escapeRegExp(query)})`, "gi");
            return text.replace(regex, "<strong>$1</strong>");
        } catch (e) {
            return text;
        }
    }

    escapeRegExp(string) {
        return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"); 
    }

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


        if (e.key === "ArrowDown") {
            e.preventDefault();
            if (items.length === 0 && this.input.value.length >= this.minCharacters) {
                this.searchSuggestions(this.input.value.trim());
                return; 
            }
            this.currentFocus++;
            if (this.currentFocus >= items.length) this.currentFocus = 0;
            this.setActiveItem(items);
        } else if (e.key === "ArrowUp") {
            e.preventDefault();
            if (items.length === 0) return;
            this.currentFocus--;
            if (this.currentFocus < 0) this.currentFocus = items.length - 1;
            this.setActiveItem(items);
        } else if (e.key === "Enter") {
            if (this.currentFocus > -1 && items[this.currentFocus]) {
                e.preventDefault(); 
                const text = items[this.currentFocus].querySelector(
                    ".suggestion-text"
                ).textContent;
                this.selectSuggestion(text); 
            } else {
                this.hideDropdown(); 
            }
        } else if (e.key === "Escape") {
            e.preventDefault();
            this.hideDropdown();
        }
    }

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

    selectSuggestion(text) {
        this.input.value = text;
        this.hideDropdown();
    }

    showDropdown() {
        this.dropdown.classList.remove("hidden");
    }

    hideDropdown() {
        this.dropdown.classList.add("hidden");
        this.currentFocus = -1;
    }

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

    getAntiForgeryToken() {
        const tokenElement = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        );
        return tokenElement ? tokenElement.value : "";
    }
}

// Initialize when DOM is loaded
document.addEventListener("DOMContentLoaded", () => {
    new Autocomplete("input-destinatie", "dropdown-destinatie", "destinatii");
    new Autocomplete("input-puncte-plecare", "dropdown-puncte-plecare", "puncte_plecare");
});
