-- Charles Farris
-- BridgeButton.lua for Roblox Studio
-- Set up a script to attach to in game bridge activation button

-- Variables to hold the button and bridge parts
local button = script.Parent
local bridge = game.Workspace.Bridge

-- Create function to press and unpress button to make the bridge walkable/unwalkable
local function ButtonPressed()
	
	-- If bridge can be walked on and the button is pressed
	if bridge.CanCollide then
		button.BrickColor = BrickColor.Red()
		-- Make bridge see through (Transparency = 1 is invisible)
		bridge.Transparency = 0.7 
		bridge.CanCollide = false
		
	else -- If bridge cannot be walked on and the button is pressed
		button.BrickColor = BrickColor.Green()
		bridge.Transparency = 0
		bridge.CanCollide = true
	end
end

-- Call the function when the button is touched
button.Touched:Connect(ButtonPressed)