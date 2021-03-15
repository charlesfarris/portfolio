-- Charles Farris
-- ColorCHange.lua for Roblox Studio
-- Create a looping random color changing brick

-- Create a variable looping part with the value of the post part
local loopingPart = script.Parent

-- Loop that runs forever
while true do 
	-- Change to a random brick color
	loopingPart.BrickColor = BrickColor.Random()
	wait(1) -- second
end
