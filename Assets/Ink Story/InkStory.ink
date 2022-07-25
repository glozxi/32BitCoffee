VAR Outcome = 0
VAR Drink = "Latte"

->start
=== start ===
-> Tutorial
=== Tutorial ===
...#Char:None #BGM:cafe#DAY:1
Oh! Hi! Welcome!#Char:Arity #MODEL:Arity,arity_default,default,MM #BGM:menu
Thank you for starting the game.#Char:Arity
Well, let me not hold you back any further, I'll..#Char:Arity #FX:phone_vibrate
Phone rings#Char:None#MODEL:Arity,HIDE,,
Hold on let me get this.#Char:Arity #MODEL:Arity,arity_default,blank,MM
Sorry.#Char:Arity
Hey whats up.#Char:Arity #MODEL:Arity,arity_default,stare,MM #FX:phone_pick
HAH!!!!??!??#Char:Arity #MODEL:Arity,arity_default,angry,MM
What do you mean you lost the story!?!?#Char:Arity 
Didn't you have a backup of it or something?!?#Char:Arity #MODEL:Arity,arity_default,sad,MM
Dammit! #Char:Arity
Phone closes#Char:None#MODEL:Arity,HIDE,,
Sorry to keep you waiting.#Char:Arity #MODEL:Arity,arity_default,default,MM
It appears the story isn't ready yet.#Char:Arity#MODEL:Arity,arity_default,default,MM
But we can demonstrate our features. 
-> Brew_Req
=== Brew_Req ===
{Be sure to read the tutorial! | Eh, can you brew again?}#Char:Arity #TOBREW:tut_MS3#MODEL:Arity,arity_default,default,MM

//Go to brew
//Comeback
-> Tut_End
=== Tut_End ===
{
- Outcome != 0 &&  Outcome != 1  && Outcome != -1:
    ~ Drink = "..."
}
Here you go.#Char:You #Sprite:FX: Serve Cup #Fx:Serve.mp3

{
- Outcome == 1 : -> Tutorial_Pos_MS2
- Outcome == 0 : -> Tutorial_Nor_MS2
- Outcome == -1 : -> Tutorial_Bad_MS2
- else : -> Tutorial_Fail_MS2
}

=== Tutorial_Pos_MS2 ===
Huh, a {Drink}?#Char:Arity#MODEL:Arity,arity_default,default,MM
Not what I ordered, but...#Char:Arity#Char:Arity #MODEL:Arity,arity_default,blank,MM
He sips.#Char:None#MODEL:Arity,HIDE,,
Wow, it's really good.#Char:Arity#MODEL:Arity,arity_default,default,MM
-> Day_1_pt_1_1

=== Tutorial_Nor_MS2 ===
Cool, a {Drink}, just what I ordered.#Char:Arity#MODEL:Arity,arity_default,default,MM
He sips.#Char:None#MODEL:Arity,HIDE,,
-> Day_1_pt_1_1

=== Tutorial_Bad_MS2 ===
The colour seems wrong.#Char:Arity#MODEL:Arity,arity_default,default,MM
Whatever.#Char:Arity#MODEL:Arity,arity_default,default,MM
He sips.#Char:None#MODEL:Arity,HIDE,,
Geez, a {Drink}?#Char:Arity#MODEL:Arity,arity_default,angry,MM
Why did you give this to me?#Char:Arity#MODEL:Arity,arity_default,angry,MM
-> Day_1_pt_1_1

=== Tutorial_Fail_MS2 ===
Hey, this doesn't seem right.#Char:Arity#MODEL:Arity,arity_default,default,MM
He peers inside the cup.#Char:None#MODEL:Arity,HIDE,,
-> Brew_Req

=== Day_1_pt_1_1 ===
Alright, cool.#Char:Arity#MODEL:Arity,arity_default,default,MM
*   Sure.
*   Yep.
- Now, let's try something harder.#TOBREW:Level1
You passed.
Good job.
Before you try brewing again, how about upgrading your parts?#UPGRADE:
Now try a real challenge!#TOBREW:Level2
#MODEL:Arity,HIDE,,
-> END