# STL
Tokenizer and runtime for the Turtle Scripting Language.

# Sample Program
```
// Turtle Scripting Launguage
var dis = 50
var turn = dis + 30

/*
	Main code
*/
Draw(true)
moveLeft(50.5 + dis)

function moveLeft (distance = 0, hi = "hi bob") // For moving left
{
	distance += 5
	
	if (distance >= 50)
	{
		Turn(turn)
		Move(distance)
	} 
	else 
	{
		Print("to small")
	}
	
	
	Print(hi)
}
```

```
Turtle Instruction Set

func moveLeft paramd moveLeft:distance num 0 paramd moveLeft:hi str "hi bob"
	seta moveLeft:distance ADD num 5
	
	if gte get moveLeft:distance num 50
		Std:Turn get turn
		Std:Move get moveLeft:distance
	else
		Std:Print str "to small"
	stop
	
	Std:Print get moveLeft:hi
stop

var dis num 50
var turn add num 30 get dis

call Std:Draw bool 1
call moveLeft add num 50.5 get dis
```
