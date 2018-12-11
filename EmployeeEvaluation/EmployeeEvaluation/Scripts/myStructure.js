var canvas;
var context;
var levelX;
var levelY;
var manager;
var parentY = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];

function myStructure(structure) {

    canvas = document.getElementById('myCanvas');
    context = canvas.getContext('2d');
    levelX = 0;
    levelY = 0;
    y1 = 0;

    context.canvas.height = structure.TeamCount * 80 + 10 + (structure.PersonCount * 80 + 10);
    myPerson(structure.Boss);
}

function myPerson(person) {
    drawPerson(person.FirstName,
            person.LastName,
            person.Position
    );
    parentY[levelX] = levelY;
    levelX++;
    manager = person.FirstName + " " + person.LastName;
    person.Teams.forEach(myTeams);
    levelX--;
}

function myTeams(team) {
    var x = levelX * 250 + 10;
    var y = levelY * 80 + 10;

    context.font = "20px Arial";
    context.fillStyle = "black";
    context.fillText(team.Name, x + 10, y + 40);

    context.font = "18px Arial bold italic";
    context.fillText(manager, x + 10, y + 63);
    context.fillStyle = "black";
    levelY++;

    context.strokeStyle = "navy";
    context.beginPath();
    context.moveTo(x - 150, parentY[levelX - 1] * 80 - 10);
    context.lineTo(x - 150, y + 50);
    context.lineTo(x - 10, y + 50);
    context.stroke();

    //var prevX = levelX;
    //var prevY = levelY;

    team.Persons.forEach(myPerson);

    //context.strokeStyle = "#FF0000";
    //context.beginPath();
    //context.moveTo(prevX * 250 - 50, prevY * 80 + 10);
    //context.lineTo(levelX * 250 - 50, (levelY) * 80 + 10);
    //context.stroke();
}


function drawPerson(firstName, lastName, position) {

    var y = levelY * 80 + 10;
    var x = levelX * 250 + 10;

    context.fillStyle = "lightblue";
    context.strokeStyle = "grey";
    context.lineWidth = "1";
    context.fillRect(x, y, 280, 48);
    context.strokeRect(x, y, 280, 48);

    context.font = "14px Arial bold italic";
    context.fillStyle = "black";
    context.fillText(position, x + 10, y + 20);

    context.font = "15px Arial";
    context.fillStyle = "black";
    context.fillText(firstName + " " + lastName, x + 10, y + 38);

    levelY++;
}