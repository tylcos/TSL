# STL
Tokenizer and runtime for the Turtle Scripting Language.

# Sample Program
```
// Turtle Scripting Launguage example program
var dis = 50
var turn = dis + 30

/*
	Main code
*/
Draw(true)
moveLeft(50.5 + dis)

function moveLeft (distance = 0, msg = "test") // For moving left
{
	distance += 5
	
	if (distance >= 50)
	{
		Turn(turn)
		Move(distance)
	} 
	else 
	{
		Print("Distance to small")
	}
	
	Print(msg)
}
```