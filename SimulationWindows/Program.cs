#if USE_MONOGAME
using System;

Console.WriteLine("Using Monogame");
#endif

using var game = new SimulationGraphique.LeJeu();
game.Run();
