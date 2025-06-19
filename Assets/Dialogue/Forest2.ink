INCLUDE globals.ink

->main

=== main === 
You hear meowing coming from a hole a few feet away. #speaker:
"It looks like it’s coming from that hole, but it's too dark inside to see." #speaker: You
"Maybe I have something I can use to help me see down there."
(You can drag items from your inventory) #speaker:Tip
->END

=== flashlightUsed ===
You turn on the flashlight and slowly bring over the hole. The light reveals a cat at the bottom. You look into her frightened brown eyes. #speaker:
"Poor kitty, it's too far down to reach. How can I convince it to come a little closer?" #spear: You
-> END

=== treatUsed ===
You place the treat near the top of the hole. The cat's fur is all standing on end but it carefully moves closer to eat.  #speaker:
"I think I can reach the cat now. I should prepare the carrier so I can put the cat in quickly." #speaker:You
-> END

=== cantUseYet ===
"Hmm...I don't think that will be helpful right now. Maybe I can use it later."  #speaker:You
-> END

=== carrierUsed ===
"Great! Now I can safely rescue this poor cat." #speaker:You
You look closer at the cat in the carrier. It looks like a stray since it doesn't have any collar. #speaker:
"I'll take it to the rescue center to give it a full check up." #speaker:You
~ blackOut()
-> END
