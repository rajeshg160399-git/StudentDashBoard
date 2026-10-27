/**
 * site.js - Global JavaScript for Student Dashboard
 * Handles global UI behavior, alert notifications, and utility functions.
 */

document.addEventListener('DOMContentLoaded', () => {
    initAlertAutoDismiss(5000);
});

/**
 * Automatically dismisses Bootstrap alerts after a specified timeout.
 * @param {number} timeoutMs - Duration in milliseconds before fading out the alert.
 */
function initAlertAutoDismiss(timeoutMs = 5000) {
    const alerts = document.querySelectorAll('.alert-dismissible');
    alerts.forEach((alert) => {
        setTimeout(() => {
            if (alert && alert.parentElement) {
                alert.classList.remove('show');
                alert.classList.add('fade');
                setTimeout(() => alert.remove(), 300);
            }
        }, timeoutMs);
    });
}

/**
 * Utility helper to format a date string into a localized readable format.
 * @param {string|Date} dateInput
 * @returns {string}
 */
function formatDate(dateInput) {
    if (!dateInput) return '';
    const date = new Date(dateInput);
    return isNaN(date.getTime()) ? '' : date.toLocaleDateString();
}
