document.getElementById("startButton").addEventListener("click", startGame);
const numbers = [
    "1", "1", "2", "2", "3", "3", "4", "4", "5", "5",
    "6", "6", "7", "7", "8", "8", "9", "9", "10", "10",
    "11", "11", "12", "12", "13", "13", "14", "14",
    "15", "15", "16", "16", "17", "17", "18", "18",
    "19", "19", "20", "20"
];
let firstClicked = null;
let secondClicked = null;
let currentTurn = 'Player 1';
let scores = { 'Player 1': 0, 'Player 2': 0 };
let gameStarted = false;
createGameBoard();
function createGameBoard() {
    const board = document.getElementById('gameBoard');
    numbers.sort(() => Math.random() - 0.5);
    board.innerHTML = '';
    numbers.forEach(num => {
        const card = document.createElement('div');
        card.classList.add('card', 'disabled');
        card.dataset.number = num;
        card.addEventListener('click', clickCard);
        board.appendChild(card);
    });
}
function startGame() {
    gameStarted = true;
    resetGame();
    createGameBoard();
    enableCards();
    updateCurrentTurn();
}
function resetGame() {
    firstClicked = null;
    secondClicked = null;
    document.getElementById('scorePlayer1').innerText = scores['Player 1'].toString();
    document.getElementById('scorePlayer2').innerText = scores['Player 2'].toString();
    document.getElementById('currentTurn').innerText = `Current Turn: ${currentTurn}`;
}
function enableCards() {
    const cards = document.querySelectorAll('.card');
    cards.forEach(card => {
        card.classList.remove('disabled');
    });
}
let clicksEnabled = true;
function clickCard(event) {
    const clickedCard = event.currentTarget;
    if (!gameStarted || !clicksEnabled || clickedCard.classList.contains('revealed') || clickedCard.classList.contains('disabled'))
        return;
    clickedCard.classList.add('revealed');
    clickedCard.innerText = clickedCard.dataset.number;
    if (!firstClicked) {
        firstClicked = clickedCard;
    }
    else {
        secondClicked = clickedCard;
        checkForMatch();
    }
}
function checkForMatch() {
    clicksEnabled = false;
    if (firstClicked.dataset.number === secondClicked.dataset.number) {
        scores[currentTurn]++;
        updateScores();
        resetTurn();
        clicksEnabled = true;
    }
    else {
        setTimeout(() => {
            firstClicked.classList.remove('revealed');
            secondClicked.classList.remove('revealed');
            resetTurn();
            clicksEnabled = true;
        }, 1500);
    }
    updateCurrentTurn();
}
function updateScores() {
    document.getElementById('scorePlayer1').innerText = scores['Player 1'].toString();
    document.getElementById('scorePlayer2').innerText = scores['Player 2'].toString();
}
function resetTurn() {
    firstClicked = null;
    secondClicked = null;
    currentTurn = currentTurn === 'Player 1' ? 'Player 2' : 'Player 1';
    updateCurrentTurn();
}
function updateCurrentTurn() {
    document.getElementById('currentTurn').innerText = `Current Turn: ${currentTurn}`;
}
//# sourceMappingURL=MatchingGameTS.js.map