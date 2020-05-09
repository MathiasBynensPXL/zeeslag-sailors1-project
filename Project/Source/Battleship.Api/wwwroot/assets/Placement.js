 
window.addEventListener("load", loaded);


function loaded() {
	let Carrier = document.getElementById("CAR");
	Carrier.addEventListener("click", CAR);
	sessionStorage.setItem("CARIsPlaced", false);

	let Battleship = document.getElementById("BS");
	Battleship.addEventListener("click", BS);
	sessionStorage.setItem("BSIsPlaced", false);

	let Destroyer = document.getElementById("DS");
	Destroyer.addEventListener("click", DS);
	sessionStorage.setItem("DSIsPlaced", false);

	let Submarine = document.getElementById("SM");
	Submarine.addEventListener("click", SM);
	sessionStorage.setItem("SMIsPlaced", false);

	let Patrolboat = document.getElementById("PB");
	Patrolboat.addEventListener("click", PB);
	sessionStorage.setItem("PBIsPlaced", false);

	let roteren = document.getElementById("rotate");
	roteren.addEventListener("click", Rotate);
	sessionStorage.setItem("rotatie", 0);
	

}

function changeButton(boat) {
	let changeName = document.getElementById("btnReset"); 
	changeName.value = "Verplaats " + boat; 
}

function CAR() {
	let boat = "Carrier";
	sessionStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("CAR", 5);
	
}

function BS() {
	let boat = "Battleship";
	sessionStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("BS", 4);
}

function DS() {
	let boat = "Destroyer";
	sessionStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("DS", 3);
}

function SM() {
	let boat = "Submarine";
	sessionStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("SM", 3);
}

function PB() {
	let boat = "Patrolboat";
	sessionStorage.setItem("ship", boat);
	changeButton(boat);
	grid_listner("PB", 2);
}

function Rotate() {
	let rot = sessionStorage.getItem("rotatie");
	rot++;
	if (rot >= 4) {
		rot -= 4;
	} 
	sessionStorage.setItem("rotatie", rot);
}

function VisualPlaceOnGrid() {
	let lengte = sessionStorage.getItem("length");
	let segmentcoordinates = sessionStorage.getItem("shipCoordinates");
	let code = sessionStorage.getItem("code");
			//							 ()()
	let array = segmentcoordinates.split(',');
	
	for (let i = 0; i < array.length; i++) {
		let vakje = document.getElementById(array[i]);
		vakje.className = code + "actief";
	}
	

}

function Reset() {
	let code = sessionStorage.getItem("code");
	let Array = document.getElementsByClassName(code + "actief");
	while (Array.length > 0) {
		for (let i = 0; i < Array.length; i++) {
			Array[i].className = "btn";
		}
		Array = document.getElementsByClassName(code + "actief");
	}
	sessionStorage.setItem(code + "IsPlaced", false);
}

function BackendPlaceOnGrid(onSuccess) {
	//																  ()()
	let coordinates = sessionStorage.getItem("shipCoordinates").split(",");
	let segmentCoordinateArray = [];

	for (let i = 0; i < coordinates.length; i++) {
		segmentCoordinate = { "row": coordinates[i] % 10, "column": Math.floor(coordinates[i] / 10) };
		segmentCoordinateArray.push(segmentCoordinate);
	} 

	let url = "https://localhost:5001/api/games/" + sessionStorage.getItem("GameID") + "/positionship";

	fetch(url,
		{
			method: "POST",
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json',
				'Authorization': 'Bearer ' + sessionStorage.getItem("token")
			},
			body: JSON.stringify({
				'shipCode': sessionStorage.getItem("code"),
				'segmentCoordinates': segmentCoordinateArray
			})

		})
		.then((response) => {
			if (response.status == 200) {
				response.json().then(data => {
					sessionStorage.setItem("isFailure", data.isFailure);
					sessionStorage.setItem("msg", data.message);
					onSuccess();
				});
			} else {
				throw `error with status ${response.status}`;
			}
		});
	
}

function errorMessage() {
	let msg = sessionStorage.getItem("msg");
	let errorBox = document.getElementById("errorBox");
	errorBox.value = msg;
}
