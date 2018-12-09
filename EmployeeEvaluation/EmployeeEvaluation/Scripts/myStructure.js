var canvas;
var context;
var levelX;
var levelY;

function myStructure(structure) {

    canvas = document.getElementById('myCanvas');
    context = canvas.getContext('2d');
    levelX = 0;
    levelY = 0;

    context.canvas.height = (structure.TeamCount * 80 + 10) + (structure.PersonCount * 80 + 10);
    myPerson(structure.Boss, 0);

    //context.beginPath();
    //context.moveTo(0, 0);
    //context.lineTo(100, 100);
    //context.stroke();
}

function myPerson(person) {
    drawPerson(person.FirstName,
            person.LastName,
            person.Position
    );

    levelX++;
    person.Teams.forEach(myTeams);
    levelX--;

}

function myTeams(team) {
    var x = levelX * 250 + 10;
    var y = levelY * 80 + 10;

    context.font = "20px Arial";
    context.fillStyle = "black";
    context.fillText(team.Name, x + 10, y + 40);
    levelY++;
    team.Persons.forEach(myPerson);
}


function drawPerson(firstName, lastName, position) {

    var y = levelY * 80 + 10;
    var x = levelX * 250 + 10;

    context.fillStyle = "lightblue";
    context.strokeStyle = "grey";
    context.lineWidth = "1";
    context.fillRect(x, y, 180, 60);
    context.strokeRect(x, y, 180, 60);

    context.font = "15px Arial";
    context.fillStyle = "black";
    context.fillText(position, x + 10, y + 20);
    context.fillText(firstName, x + 10, y + 35);
    context.fillText(lastName, x + 10, y + 50);

    levelY++;
}