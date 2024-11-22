document.addEventListener('DOMContentLoaded', () => {
    const addQuestionBtn = document.getElementById('add-question-btn');
    const questionsDiv = document.getElementById('questions');
    const quizForm = document.getElementById('quiz-form');

    addQuestionBtn.addEventListener('click', () => {
        const questionDiv = document.createElement('div');
        questionDiv.innerHTML = `
            <label>Question Text:</label>
            <input type="text" name="question-text[]" required>
            
            <label>Options (comma separated):</label>
            <input type="text" name="options[]" required>
            
            <label>Correct Option Index (0-based):</label>
            <input type="number" name="correct-option-index[]" required min="0">
            
            <hr>
        `;
        questionsDiv.appendChild(questionDiv);
    });

    quizForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(quizForm);

        const quiz = {
            title: formData.get('title'),
            questions: []
        };

        const questionTexts = formData.getAll('question-text[]');
        const optionsList = formData.getAll('options[]');
        const correctOptionIndexes = formData.getAll('correct-option-index[]');

        questionTexts.forEach((text, index) => {
            quiz.questions.push({
                text,
                options: optionsList[index].split(','),
                correctOptionIndex: parseInt(correctOptionIndexes[index])
            });
        });

        fetch('/api/quizzes', {  // Assuming your POST API for quiz creation is at this endpoint
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(quiz)
        })
            .then(response => response.json())
            .then(data => {
                alert('Quiz created successfully!');
                window.location.href = '/';
            })
            .catch(error => console.error('Error creating quiz:', error));
    });
});
