TODO // Just for testing, can change
Hey, what's going on?
It's dark.
-> start
=== start ===
*   [Look left] -> option
*   [Look right] -> option
*   -> end_option
= option
Well, {there's nothing there| nothing there either}.
-> start
= end_option
Wake up! From now on, you work for me!
Wait wait wait what?
Who are you?
I'm Xiao Mei, your new boss.
You're gonna work in my cafe.
A cafe? I don't remember anything about a cafe.
Come to think of it, I can't remember anything at all.
*   [Help, I think I have amnesia.]
*   [Ahh!]
-   Huh, this one is certainly emotional.
The bottom-line is this.
You're the barista. 
You'll have to make coffee to serve to the customers.
Coffee.
When I hear that word 'coffee', my mind stops racing.
Barista, huh.
That seems about right.
-> start_questions

=== start_questions ===
Any {|more} questions?
*   [Where am I? And when?]
-> describe_place
*   [Who are you? ]
-> describe_boss
*   [Who am I? ]
-> describe_player
*   -> END
->start_questions

= describe_place
You're in Newgapore, the city of cool stuff.
To be specific, you're in this cafe in Newgapore.
For the date, it's 1 June 2099.
-> start_questions

= describe_boss
I own this cafe.
-> start_questions

= describe_player
You are a robot.
They didn't program that into you?
Hold up, I'm a robot?
-> start_questions

-> END
