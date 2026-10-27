/**
 * account.js - Module-specific JavaScript for Account authentication & registration
 * Provides client-side validation helpers and password interactivity.
 */

document.addEventListener('DOMContentLoaded', () => {
    initPasswordMatchValidation();
    initPasswordToggle();
});

/**
 * Event Handler & Function: Validates password match in real-time.
 */
function initPasswordMatchValidation() {
    const passwordInput = document.getElementById('Password');
    const confirmPasswordInput = document.getElementById('ConfirmPassword');
    const feedbackSpan = document.getElementById('confirmPasswordFeedback');

    if (passwordInput && confirmPasswordInput) {
        const validate = () => {
            const pwd = passwordInput.value;
            const confirmPwd = confirmPasswordInput.value;

            if (!confirmPwd) {
                if (feedbackSpan) feedbackSpan.textContent = '';
                confirmPasswordInput.classList.remove('is-invalid', 'is-valid');
                return;
            }

            if (pwd === confirmPwd) {
                confirmPasswordInput.classList.remove('is-invalid');
                confirmPasswordInput.classList.add('is-valid');
                if (feedbackSpan) {
                    feedbackSpan.textContent = 'Passwords match';
                    feedbackSpan.className = 'text-success small';
                }
            } else {
                confirmPasswordInput.classList.remove('is-valid');
                confirmPasswordInput.classList.add('is-invalid');
                if (feedbackSpan) {
                    feedbackSpan.textContent = 'Passwords do not match';
                    feedbackSpan.className = 'text-danger small';
                }
            }
        };

        // Event Listeners for interactive validation
        confirmPasswordInput.addEventListener('input', validate);
        passwordInput.addEventListener('input', () => {
            if (confirmPasswordInput.value) validate();
        });
    }
}

/**
 * Initializes toggle buttons to show/hide password text.
 */
function initPasswordToggle() {
    const toggleButtons = document.querySelectorAll('.toggle-password-btn');
    toggleButtons.forEach((btn) => {
        btn.addEventListener('click', (e) => {
            e.preventDefault();
            const targetId = btn.getAttribute('data-target');
            const targetInput = document.getElementById(targetId);
            if (targetInput) {
                const isPassword = targetInput.type === 'password';
                targetInput.type = isPassword ? 'text' : 'password';
                btn.textContent = isPassword ? 'Hide' : 'Show';
            }
        });
    });
}
