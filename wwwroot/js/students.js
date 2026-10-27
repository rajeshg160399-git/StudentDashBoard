/**
 * students.js - Module-specific JavaScript for Student management
 * Contains student table filtering, search event handlers, and API integration.
 */

document.addEventListener('DOMContentLoaded', () => {
    initStudentSearch();
    fetchStudentCount();
});

/**
 * Initializes real-time search and event listeners for the student directory table.
 */
function initStudentSearch() {
    const searchInput = document.getElementById('studentSearchInput');
    const clearBtn = document.getElementById('clearSearchBtn');

    if (searchInput) {
        // Event Handler: real-time filter on input
        searchInput.addEventListener('input', (e) => {
            const query = e.target.value.trim().toLowerCase();
            filterStudents(query);
            if (clearBtn) {
                clearBtn.style.display = query.length > 0 ? 'inline-block' : 'none';
            }
        });

        // Event Handler: clear search on Escape key
        searchInput.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') {
                searchInput.value = '';
                filterStudents('');
                if (clearBtn) clearBtn.style.display = 'none';
            }
        });
    }

    if (clearBtn) {
        // Event Handler: reset filter on click
        clearBtn.addEventListener('click', () => {
            if (searchInput) {
                searchInput.value = '';
                searchInput.focus();
            }
            filterStudents('');
            clearBtn.style.display = 'none';
        });
    }
}

/**
 * JavaScript Function: Filters student table rows based on search term.
 * Checks student name, enrollment number, course, and email.
 * @param {string} query - The search term to match against.
 */
function filterStudents(query) {
    const table = document.getElementById('studentsTable');
    if (!table) return;

    const rows = table.querySelectorAll('tbody tr.student-row');
    const noResultsRow = document.getElementById('noMatchingStudentsRow');
    let visibleCount = 0;

    rows.forEach((row) => {
        const text = row.textContent.toLowerCase();
        const matches = !query || text.includes(query);
        row.style.display = matches ? '' : 'none';
        if (matches) visibleCount++;
    });

    if (noResultsRow) {
        noResultsRow.style.display = (visibleCount === 0 && rows.length > 0) ? '' : 'none';
    }

    const countBadge = document.getElementById('filteredCountBadge');
    if (countBadge) {
        countBadge.textContent = query ? `${visibleCount} found` : '';
        countBadge.style.display = query ? 'inline-block' : 'none';
    }
}

/**
 * API Call: Asynchronously fetches live student statistics from the server.
 * Updates the student count badge in the user interface.
 */
async function fetchStudentCount() {
    const badge = document.getElementById('liveStudentCountBadge');
    if (!badge) return;

    try {
        const response = await fetch('/Students/GetStudentCount', {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`Server returned HTTP status ${response.status}`);
        }

        const data = await response.json();
        if (data && typeof data.totalStudents === 'number') {
            badge.textContent = `${data.totalStudents} total students registered`;
            badge.classList.remove('d-none');
        }
    } catch (error) {
        console.warn('Could not load student stats via API:', error);
    }
}
