/**
 * reports.js - Module-specific JavaScript for Reports
 * Provides report data export to CSV, print capabilities, and event handling.
 */

document.addEventListener('DOMContentLoaded', () => {
    initReportActions();
});

/**
 * Binds event listeners for report export and printing.
 */
function initReportActions() {
    const exportBtn = document.getElementById('exportCsvBtn');
    const printBtn = document.getElementById('printReportBtn');

    if (exportBtn) {
        exportBtn.addEventListener('click', () => {
            exportTableToCsv('reportsTable', 'student-enrollment-report.csv');
        });
    }

    if (printBtn) {
        printBtn.addEventListener('click', () => {
            window.print();
        });
    }
}

/**
 * JavaScript Function: Exports an HTML table to a downloadable CSV file.
 * @param {string} tableId - HTML element ID of the table to export.
 * @param {string} filename - Desired name for the downloaded file.
 */
function exportTableToCsv(tableId, filename = 'report.csv') {
    const table = document.getElementById(tableId);
    if (!table) return;

    const rows = Array.from(table.querySelectorAll('tr'));
    const csvContent = rows.map((row) => {
        const cells = Array.from(row.querySelectorAll('th, td'));
        return cells.map((cell) => {
            let text = cell.innerText.replace(/"/g, '""').trim();
            return `"${text}"`;
        }).join(',');
    }).join('\r\n');

    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.setAttribute('href', url);
    link.setAttribute('download', filename);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
