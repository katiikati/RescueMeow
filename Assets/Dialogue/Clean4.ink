INCLUDE globals.ink

->main

=== main === 
{catName} has so much dirt on her fur! #speaker:You
I'll give her a bath to clean her up.
(Drag the soap to clean {catName})
->END

=== catCleaned ===
{catName} is purr-fectly clean now!  #speaker:You
Look at that beautiful kitty :)
~ blackOut()
-> END
