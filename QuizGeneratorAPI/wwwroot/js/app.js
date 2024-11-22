//document.addEventListener('DOMContentLoaded', () => {
//    fetch('/api/quizzes')  // Assuming your API is available at this endpoint
//        .then(response => response.json())
//        .then(quizzes => {
//            const quizList = document.getElementById('quizzes');
//            quizzes.forEach(quiz => {
//                const li = document.createElement('li');
//                li.textContent = quiz.title;
//                quizList.appendChild(li);
//            });
//        })
//        .catch(error => console.error('Error fetching quizzes:', error));
//});


// app.js


//const baseUrl = 'http://localhost:5000/api/quizzes'; // Your API base URL

//// Elements
//const createQuizBtn = document.getElementById('createQuizBtn');
//const quizFormContainer = document.getElementById('quizFormContainer');
//const quizForm = document.getElementById('quizForm');
//const quizList = document.getElementById('quizList');
//let currentQuizId = null;

//// Show/Hide the Quiz Form
//createQuizBtn.addEventListener('click', () => {
//    quizFormContainer.style.display = 'block';
//});

//// Fetch All Quizzes
//async function fetchQuizzes() {
//    const response = await fetch(baseUrl);
//    const quizzes = await response.json();
//    quizList.innerHTML = '';
//    quizzes.forEach(quiz => {
//        const quizItem = document.createElement('li');
//        quizItem.innerHTML = `
//      <span>${quiz.title}</span>
//      <div>
//        <button class="edit-btn" onclick="editQuiz(${quiz.id})">Edit</button>
//        <button class="delete-btn" onclick="deleteQuiz(${quiz.id})">Delete</button>
//      </div>
//    `;
//        quizList.appendChild(quizItem);
//    });
//}

//// Create or Update Quiz
//quizForm.addEventListener('submit', async (event) => {
//    event.preventDefault();
//    const title = document.getElementById('quizTitle').value;
//    const numberOfQuestions = document.getElementById('quizQuestions').value;

//    const quizData = {
//        title,
//        questions: Array.from({ length: numberOfQuestions }).map(() => ({
//            text: "New Question",
//            options: ["Option 1", "Option 2", "Option 3", "Option 4"],
//            correctOptionIndex: 0
//        }))
//    };

//    const method = currentQuizId ? 'PUT' : 'POST';
//    const url = currentQuizId ? `${baseUrl}/${currentQuizId}` : baseUrl;

//    const response = await fetch(url, {
//        method,
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(quizData)
//    });

//    const result = await response.json();
//    if (response.ok) {
//        alert(`${method === 'POST' ? 'Quiz Created' : 'Quiz Updated'} Successfully!`);
//        fetchQuizzes(); // Refresh Quiz List
//        quizForm.reset();
//        quizFormContainer.style.display = 'none';
//        currentQuizId = null;
//    } else {
//        alert('Error: ' + result.message);
//    }
//});

//// Edit Quiz
//function editQuiz(id) {
//    currentQuizId = id;
//    fetch(`${baseUrl}/${id}`)
//        .then(response => response.json())
//        .then(quiz => {
//            document.getElementById('quizTitle').value = quiz.title;
//            document.getElementById('quizQuestions').value = quiz.questions.length;
//            quizFormContainer.style.display = 'block';
//        });
//}

//// Delete Quiz
//async function deleteQuiz(id) {
//    const response = await fetch(`${baseUrl}/${id}`, {
//        method: 'DELETE'
//    });

//    if (response.ok) {
//        alert('Quiz Deleted Successfully!');
//        fetchQuizzes(); // Refresh Quiz List
//    } else {
//        alert('Error: Could not delete quiz.');
//    }
//}

//// Initial Fetch Quizzes
//fetchQuizzes();


// app.js
const baseUrl = 'http://localhost:5000/api/quizzes'; // Adjust your backend URL if needed

// Elements
const createQuizBtn = document.getElementById('createQuizBtn');
const quizFormContainer = document.getElementById('quizFormContainer');
const quizForm = document.getElementById('quizForm');
const quizList = document.getElementById('quizList');
let currentQuizId = null;

// Show/Hide the Quiz Form
createQuizBtn.addEventListener('click', () => {
    quizFormContainer.style.display = 'block';
    clearForm();
});

// Fetch All Quizzes
async function fetchQuizzes() {
    const response = await fetch(baseUrl);
    const quizzes = await response.json();
    quizList.innerHTML = '';
    quizzes.forEach(quiz => {
        const quizItem = document.createElement('li');
        quizItem.innerHTML = `
      <span>${quiz.title} - ${quiz.questions.length} Questions</span>
      <div>
        <button class="edit-btn" onclick="editQuiz(${quiz.id})">Edit</button>
        <button class="delete-btn" onclick="deleteQuiz(${quiz.id})">Delete</button>
      </div>
    `;
        quizList.appendChild(quizItem);
    });
}

// Create or Update Quiz
quizForm.addEventListener('submit', async (event) => {
    event.preventDefault();
    const title = document.getElementById('quizTitle').value;
    const numberOfQuestions = document.getElementById('quizQuestions').value;

    const quizData = {
        title,
        questions: Array.from({ length: numberOfQuestions }).map(() => ({
            text: "New Question",
            options: ["Option 1", "Option 2", "Option 3", "Option 4"],
            correctOptionIndex: 0
        }))
    };

    const method = currentQuizId ? 'PUT' : 'POST';
    const url = currentQuizId ? `${baseUrl}/${currentQuizId}` : baseUrl;

    const response = await fetch(url, {
        method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(quizData)
    });

    if (response.ok) {
        alert(currentQuizId ? 'Quiz Updated Successfully!' : 'Quiz Created Successfully!');
        clearForm();
        fetchQuizzes();
    }
});

// Edit Quiz
function editQuiz(id) {
    currentQuizId = id;
    fetch(`${baseUrl}/${id}`)
        .then(response => response.json())
        .then(quiz => {
            document.getElementById('quizTitle').value = quiz.title;
            document.getElementById('quizQuestions').value = quiz.questions.length;
            quizFormContainer.style.display = 'block';
        });
}

// Delete Quiz
async function deleteQuiz(id) {
    const response = await fetch(`${baseUrl}/${id}`, {
        method: 'DELETE'
    });

    if (response.ok) {
        alert('Quiz Deleted Successfully!');
        fetchQuizzes();
    } else {
        alert('Failed to Delete Quiz');
    }
}

// Clear form
function clearForm() {
    document.getElementById('quizTitle').value = '';
    document.getElementById('quizQuestions').value = '';
    currentQuizId = null;
    quizFormContainer.style.display = 'none';
}

// Initial fetch of quizzes
fetchQuizzes();
