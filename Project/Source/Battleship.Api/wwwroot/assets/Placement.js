
window.addEventListener("load", loaded);


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
	
	grid_listner("CAR",5);
}

function BS() {
	
	grid_listner("BS", 4);
}

function DS() {
	
	grid_listner("DS", 3);
}

function SM() {
	
	grid_listner("SM", 3);
}

function PB() {

	grid_listner("PB", 2);
}


