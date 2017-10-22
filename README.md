# STL
Tokenizer and runtime for the Turtle Scripting Language.

# Sample Program
```
// Variables
var dis = 50
var turn = dis + 30

/*
	Main code
*/
Draw(true)
moveLeft(50.5 + dis)

function moveLeft (distance) // For moving left
{
	Turn(turn)
	Move(distance)
}
```
