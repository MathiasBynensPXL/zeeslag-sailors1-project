
window.addEventListener("load", loaded);

function myFunction() {
	alert("error");

}

//let grid = localStorage.getItem("gameboard");

//grid.addEventListener("click", (event) => {
//	let isButton = event.target.nodeName === 'BUTTON';
//	if (!isButton) {
//		return;
//	}
//	alert(event.target.id);
//})




function create_listner() {

	//let nummer = 0;

	//for (let i = 0; i < cols - 1; i++) {

	//	for (let j = 0; j < rows - 1; j++) {

	//		nummer = i + "" + j;
	//		let getal = document.getElementById(nummer);

	//		getal.addEventListener("click", myFunction());
	//		document.getElementsByTagName("button");
			
	//	}

	//}

	let buttons = document.getElementsByTagName("button");
	
	alert(buttons.length);
	//let squareId = event.target.id;
	//alert(squareId);
		
	

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
	create_listner();
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
	alert(length);
	alert(code);

}

