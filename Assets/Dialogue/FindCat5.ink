INCLUDE globals.ink

->main

=== main === 
{catName} just returned from seeing the vet and dashed off when the carrier door came loose.  #speaker:
"She needs to take her medication, I've got to find her!" #speaker:You
->END

=== foundCat ===
"There you are {catName}!"  #speaker:You
"You must be scared from that vet visit. I'm sorry but I've got to give you your medication" #speaker:You
Maybe some food and water will help calm her nerves. #speaker:
~ blackOut()
->END
