// set grid rows and columns and the size of each square

let rows = 11;
let cols = 11;
let squareSize = 50;

// get the container element
let gameBoardContainer = document.getElementById("gameboard");
let gameBoardContainer2 = document.getElementById("gameboard2");
// make the grid columns and rows

for (let i = 0; i < cols; i++)
{

    for (let j = 0; j < rows; j++)
    {

        let square = document.createElement("button");
        gameBoardContainer.appendChild(square);
        
     

        let square2 = document.createElement("div");
        gameBoardContainer2.appendChild(square2);
        
        if (i == 0) {
                square.id = "Y" + j;
                square2.id = "y" + j;
        }          
        if (j == 0) {
            square.id = 'X' + i;
            square2.id = 'x' + i;
        } else if (i != 0 && j != 0) {
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

function myFunction()
{
    alert("testt" + string);

}
let string = "00";
let teller = 1;
for (let i = 0; i < cols * rows; i++)
{
    document.getElementById(string).addEventListener("click", myFunction);
    string = "0" + teller;
    teller++;
}

for (let k = 1; k <= 10; k++)
{

    document.getElementById("X" + k).innerText = k.toString();
    document.getElementById("x" + k).innerHTML = k.toString();
}
for (let l = 0; l <= 10; l++) {
    if (document.getElementById("Y" + l) || document.getElementById("y" + l)) {
        for (let m = 65; m <= 75; m++) {
            document.getElementById("Y" + l).innerHTML = String.fromCharCode(m);
            document.getElementById("y" + l).innerHTML = String.fromCharCode(m);
            l++;
        }
    }
}





