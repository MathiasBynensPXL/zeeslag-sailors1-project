window.addEventListener("load", loaded);
onmouseover = function () {
	(getId);
}

function getId() {
	let find = this;
	if (find == "99") {
		alert("you did it!");
    }
}

function loaded() {
	let Carrier = document.getElementById("CAR");
	Carrier.addEventListener("click", CAR);
	let Battleship = document.getElementById("BS");
	Battleship.addEventListener("click", BS);
	let Destroyer = document.getElementById("DS");
	Destroyer.addEventListener("click", DS);
	let Submarine = document.getElementById("SM");
	Submarine.addEventListener("click", SM);
	let Patrolboat = document.getElementById("PB");
	Patrolboat.addEventListener("click", PB);

}



function CAR() {
	GridListener("CAR", 5);
}

function BS() {
	GridListener("BS", 4);
}

function DS() {
	GridListener("DS", 3);
}

function SM() {
	GridListener("SM", 3);
}

function PB() {
	GridListener("PB", 2);
}

function GridListener(code, length) {
	//let Grid = document.getElementById("gameboard")
	//Grid.addEventListener("mouseover");
	for (let i = 0; i < 100; i++) {
		document.getElementById(i + "");
    }

	square.onclick("test(this.id)");
}
function FindCoordinate() {

}



//Game.prototype.rosterListener = function (e) {
//	let self = e.target.self;
//	// Remove all classes of 'placing' from the fleet roster first
//	let roster = document.querySelectorAll('.fleet li');
//	for (let i = 0; i < roster.length; i++) {
//		let classes = roster[i].getAttribute('class') || '';
//		classes = classes.replace('placing', '');
//		roster[i].setAttribute('class', classes);
//	}

//	// Move the highlight to the next step
//	if (gameTutorial.currentStep === 1) {
//		gameTutorial.nextStep();
//	}

//	// Set the class of the target ship to 'placing'
//	Game.placeShipType = e.target.getAttribute('id');
//	document.getElementById(Game.placeShipType).setAttribute('class', 'placing');
//	Game.placeShipDirection = parseInt(document.getElementById('rotate-button').getAttribute('data-direction'), 10);
//	self.placingOnGrid = true;
//};
