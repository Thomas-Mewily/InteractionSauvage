using Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SimulationGraphique.Managers;

public static class KeysInput 
{
    public static bool IsDown(this Keys k) => All.Input.IsDown(k);
    public static bool IsUp(this Keys k) => All.Input.IsUp(k);
    public static bool IsPressed(this Keys k) => All.Input.IsPressed(k);
    public static bool IsReleased(this Keys k) => All.Input.IsReleased(k);
}

public class Input : ClasseBase
{
    public MouseState Mouse { get; private set; }
    public MouseState MouseOld { get; private set; }

    public KeyboardState Keyboard { get; private set; }
    public KeyboardState KeyboardOld { get; private set; }

    public bool IsPressed(Keys k)   =>  Keyboard.IsKeyUp(k) && !KeyboardOld.IsKeyUp(k);
    public bool IsReleased(Keys k) => !Keyboard.IsKeyUp(k) &&  KeyboardOld.IsKeyUp(k);

    public bool IsDown(Keys k) => Keyboard.IsKeyDown(k);
    public bool IsUp(Keys k) => Keyboard.IsKeyUp(k);

    public override void Update()
    {
        MouseOld = Mouse;
        Mouse = Microsoft.Xna.Framework.Input.Mouse.GetState();

        KeyboardOld = Keyboard;
        Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }
}