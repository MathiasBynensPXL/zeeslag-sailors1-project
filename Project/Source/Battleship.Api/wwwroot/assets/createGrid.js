// set grid rows and columns and the size of each square
let rows = 11;
let cols = 11;
let squareSize = 50;

// get the container element
let gameBoardContainer = document.getElementById("gameboard");
sessionStorage.setItem("gameboard", gameboard);
let gameBoardContainer2 = document.getElementById("gameboard2");
// make the grid columns and rows

for (let i = 0; i < cols; i++)
{

    for (let j = 0; j < rows; j++)
    {

        let square = document.createElement("button");      
        gameBoardContainer.appendChild(square);
        
        let square2 = document.createElement("button");
        gameBoardContainer2.appendChild(square2);
        
        if (i == 0) {
            square.className = "noBtnY";
            square2.className = "noBtnY";
            square.id = "Y" + j;
            square2.id = "y" + j;
        }          
        if (j == 0) {
            square.className = "noBtnX";
            square2.className = "noBtnX";
            square.id = 'X' + i;
            square2.id = 'x' + i;
        } else if (i != 0 && j != 0) {
            square.className = "btn";
            square2.className = "btnComputer";
            square.id = (i-1) + "" + (j-1);
            square2.id = (i-1) + "" + (j-1);
        }

        
        // set each grid square's coordinates: multiples of the current row or column number
        let topPosition = j * squareSize;
        let leftPosition = i * squareSize;

        // use CSS absolute positioning to place each grid square on the page
        square.style.top = topPosition + 'px';
        square.style.left = leftPosition + 'px';
        square2.style.top = topPosition + 'px';
        square2.style.left = leftPosition + 'px';
       

    }
    
}

function Coordinate(x, y) {
    this.x = x;
    this.y = y;
}

function CalculatePosition() {
    let lengte = parseInt(sessionStorage.getItem("length"));
    let rotatie = parseInt(sessionStorage.getItem("rotatie"));
    let coordinaat = parseInt(sessionStorage.getItem("squareId"));
    let ship = [];
    let code = sessionStorage.getItem("code");
    let isPlaced = sessionStorage.getItem(code + "IsPlaced");
    let outOfBounds = false;

    if (isPlaced === 'false') {
        if (rotatie === 0) {
            for (let i = 0; i < lengte; i++) {
                ship[i] = coordinaat + i;
                if (ship[i] < 0 || ship[i] >= (rows - 1) * (cols - 1)) {
                    outOfBounds = true;
                }
                if (i != 0 && ship[i - 1] % 10 === 9 && ship[i] % 10 === 0) {
                    outOfBounds = true;
                }
                if (ship[i] < 10) {
                    ship[i] = "0" + ship[i];
                }
                
            }
        } else if (rotatie === 1) {
            for (let i = 0; i < lengte; i++) {
                ship[i] = coordinaat + (i * 10);
                if (ship[i] < 0 || ship[i] >= (rows - 1) * (cols - 1)) {
                    outOfBounds = true;
                }
                if (ship[i] < 10) {
                    ship[i] = "0" + ship[i];
                }
                
            }
        } else if (rotatie === 2) {
            for (let i = 0; i < lengte; i++) {
                ship[i] = coordinaat - i;
                if (ship[i] < 0 || ship[i] >= (rows - 1) * (cols - 1)) {
                    outOfBounds = true;
                }
                if (i != 0 && ship[i - 1] % 10 === 0 && ship[i] % 10 === 9) {
                    outOfBounds = true;
                }
                if (ship[i] < 10) {
                    ship[i] = "0" + ship[i];
                }
                
            }
        } else if (rotatie === 3) {
            for (let i = 0; i < lengte; i++) {
                ship[i] = coordinaat - (i * 10);
                if (ship[i] < 0 || ship[i] >= (rows - 1) * (cols - 1)) {
                    outOfBounds = true;
                }
                if (ship[i] < 10) {
                    ship[i] = "0" + ship[i];
                }
               
            }
        }
    }
    
    if (!outOfBounds && isPlaced === 'false') {
        sessionStorage.setItem("shipCoordinates", ship);
        BackendPlaceOnGrid(function () {
            errorMessage();
            let msg = sessionStorage.getItem("isFailure");
            if (msg != 'true') {
                sessionStorage.setItem(code + "IsPlaced", true);
                VisualPlaceOnGrid();
            }
        });

    } else if (outOfBounds) {
        sessionStorage.setItem("msg", "Out of bounds");
        errorMessage();
    } else {
        sessionStorage.setItem("msg", "Already placed!");
        errorMessage();
    }
}

function handleEvent() {

   let squareId = event.target.id;
   sessionStorage.setItem("squareId", squareId);
    CalculatePosition();
    
}

function grid_listner(code,length) {
    sessionStorage.setItem("code", code);
    sessionStorage.setItem("length", length);

    const buttons = document.querySelectorAll('.btn')

    buttons.forEach(function (currentBtn)
    {
    currentBtn.addEventListener('click', handleEvent);
    });

}

for (let k = 1; k < 11; k++)
{

    document.getElementById("X" + k).innerText = k.toString();
    document.getElementById("x" + k).innerHTML = k.toString();
}

for (let l = 0; l < 11; l++) {
    if (document.getElementById("Y" + l) || document.getElementById("y" + l)) {
        for (let m = 65; m < 75; m++) {
           
            document.getElementById("y" + l).innerHTML = String.fromCharCode(m);
            document.getElementById("Y" + l).innerHTML = String.fromCharCode(m);
            l++;
        }
    }
}
