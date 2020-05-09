
window.addEventListener("load", loaded);


function loaded() {
	let Carrier = document.getElementById("CAR");
	Carrier.addEventListener("click", CAR);
	localStorage.setItem("CARIsPlaced", false);

	let Battleship = document.getElementById("BS");
	Battleship.addEventListener("click", BS);
	localStorage.setItem("BSIsPlaced", false);

	let Destroyer = document.getElementById("DS");
	Destroyer.addEventListener("click", DS);
	localStorage.setItem("DSIsPlaced", false);

	let Submarine = document.getElementById("SM");
	Submarine.addEventListener("click", SM);
	localStorage.setItem("SMIsPlaced", false);

	let Patrolboat = document.getElementById("PB");
	Patrolboat.addEventListener("click", PB);
	localStorage.setItem("PBIsPlaced", false);

	let roteren = document.getElementById("rotate");
	roteren.addEventListener("click", Rotate);
	localStorage.setItem("rotatie", 0);
	

}
function changeButton(boat) {
	let changeName = document.getElementById("btnReset"); 
	changeName.value = "Reset " + boat; 
	//document.getElementById("btnReset").innerHTML = "Reset " + boat;
}

function CAR() {
	let boat = "Carrier";
	localStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("CAR", 5);
	
}

function BS() {
	let boat = "Battleship";
	localStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("BS", 4);
}

function DS() {
	let boat = "Destroyer";
	localStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("DS", 3);
}

function SM() {
	let boat = "Submarine";
	localStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("SM", 3);
}

function PB() {
	let boat = "Patrolboat";
	localStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("PB", 2);
}

function Rotate() {
	let rot = localStorage.getItem("rotatie");
	rot++;
	if (rot >= 4) {
		rot -= 4;
	} 
	localStorage.setItem("rotatie", rot);
}

function VisualPlaceOnGrid() {
	let lengte = localStorage.getItem("length");
	let segmentcoordinates = localStorage.getItem("shipCoordinates");
	let code = localStorage.getItem("code");
			//							 ()()
	let array = segmentcoordinates.split(',');
	
	for (let i = 0; i < array.length; i++) {
		let vakje = document.getElementById(array[i]);
		vakje.className = code + "actief";
	}
	

}

function Reset() {
	let code = localStorage.getItem("code");
	let Array = document.getElementsByClassName(code + "actief");
	alert(Array.length);
	while (Array.length > 0) {
		for (let i = 0; i < Array.length; i++) {
			Array[i].className = "btn";
		}
		Array = document.getElementsByClassName(code + "actief");
	}
	localStorage.setItem(code + "IsPlaced", false);
}

function BackendPlaceOnGrid() {
	let coordinates = localStorage.getItem("shipCoordinates");
	let string = '[ ';
	for (let i = 0; i < localStorage.getItem("length"); i++) {
		string += '{ "row":' + coordinates[i] % 10 + ',';
		string += '"column":' + coordinates[i] / 10 + '},';
	} 
	string += ']';

	let url = "https://localhost:5001/api/games/" + sessionStorage.getItem("GameID") + "/positionship";

	fetch(url,
		{
			method: "POST",
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json',
				'Authorization': 'Bearer ' + sessionStorage.getItem("token")
			},
			body: {
				'shipCode': localStorage.getItem("code"),
				'segmentCoordinates': string
			}

		})
		.then((response) => {
			if (response.status == 200) {
				return response.json();
			} else {
				throw `error with status ${response.status}`;
			}
		});
	
}
