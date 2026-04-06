using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: The enums MUST be listed in progression order
public enum TorchPhase
{
    Start,
    FindTorchAndRiddle,
    FindPowerOutlet,
    LightPlayerTorch,
    Complete
};

public enum CatPhase
{
    Start,
    Find1Cat,
    Find2Cat,
    FindAllCats,
    Complete
};

public enum MazePhase
{
    Start,
    FindMazeStart,
    FindMazeRoute,
    Complete
};

public enum LavaPhase
{
    Start,
    Complete
};

public enum GlyphPhase
{
    Start,
    FindLock,
    PlaceLock,
    SolveLock,
    Complete
};

public class PhasePartManager : MonoBehaviour
{

    public TorchPhase torchPhase = TorchPhase.Start;
    public CatPhase catPhase = CatPhase.Start;
    public MazePhase mazePhase = MazePhase.Start;
    public LavaPhase lavaPhase = LavaPhase.Start;
    public GlyphPhase glyphPhase = GlyphPhase.Start;

    public Dictionary<TorchPhase, string> TorchHints = new Dictionary<TorchPhase, string>();
    public Dictionary<CatPhase, string> CatHints = new Dictionary<CatPhase, string>();
    public Dictionary<MazePhase, string> MazeHints = new Dictionary<MazePhase, string>();
    public Dictionary<LavaPhase, string> LavaHints = new Dictionary<LavaPhase, string>();
    public Dictionary<GlyphPhase, string> GlyphHints = new Dictionary<GlyphPhase, string>();

    // Map each phase part to a hint
    void Start()
    {
        TorchHints.Add(TorchPhase.Start, "Look around to see if anything can help you");
        TorchHints.Add(TorchPhase.FindTorchAndRiddle, "The riddle might be about something in the physical world that helps provide energy");
        TorchHints.Add(TorchPhase.FindPowerOutlet, "The power outlet provides the torch energy! Maybe you can light your torch too");
        TorchHints.Add(TorchPhase.LightPlayerTorch, "Can you use your lit torch to open the gate?");
        TorchHints.Add(TorchPhase.Complete, "You've completed the torch puzzle!");

        CatHints.Add(CatPhase.Start, "Look something cold to help you");
        CatHints.Add(CatPhase.Find1Cat, "Look something like rainbow to help you");
        CatHints.Add(CatPhase.Find2Cat, "Look something smiling to help you");
        CatHints.Add(CatPhase.FindAllCats, "Open the map to check some hints");
        CatHints.Add(CatPhase.Complete, "You've completed the cat puzzle!");

        MazeHints.Add(MazePhase.Start, "Look around to find the start point");
        MazeHints.Add(MazePhase.FindMazeStart, "Look somthing wild to find the route");
        MazeHints.Add(MazePhase.FindMazeRoute, "Follow the route to go through the maze");
        MazeHints.Add(MazePhase.Complete, "You've completed the maze puzzle!");

        LavaHints.Add(LavaPhase.Start, "Go across the lava by placing the platforms");
        LavaHints.Add(LavaPhase.Complete, "You've completed the lava!");

        GlyphHints.Add(GlyphPhase.Start, "Go to find the lock");

        GlyphHints.Add(GlyphPhase.FindLock, "Go to the physical place where you fell into the tomb and scan the original sign");
        GlyphHints.Add(GlyphPhase.PlaceLock, "These icons seem related to a letter. Maybe you need to spell out a word?");
        GlyphHints.Add(GlyphPhase.SolveLock, "Great work on solving the glyph! Now grab that treasure and let's get out of here!");

        GlyphHints.Add(GlyphPhase.Complete, "You've completed the glyph!");
    }


    // 5 methods below provide the hint for each sub-phase
    public string GetTorchPhaseHint()
    {
        return TorchHints[torchPhase];
    }

    public string GetCatPhaseHint()
    {
        return CatHints[catPhase];
    }

    public string GetMazePhaseHint()
    {
        return MazeHints[mazePhase];

    }

    public string GetLavaPhaseHint()
    {
        return LavaHints[lavaPhase];
    }

    public string GetGlyphPhaseHint()
    {
        return GlyphHints[glyphPhase];

    }

    // 5 methods below update the phase of each puzzle in the game
    // They only update the phase if the new phase occurs after the current phase
    // Example: TorchPhase.FindPowerOutlet > TorchPhase.Start is TRUE
    // Example: TorchPhase.FindTorchAndRiddle > TorchPhase.Complete is FALSE

    public void UpdateTorchPhase(TorchPhase newPhase)
    {
        if ((int)newPhase > (int)torchPhase) torchPhase = newPhase;
    }

    public void UpdateCatPhase(CatPhase newPhase)
    {
        if ((int)newPhase > (int)catPhase) catPhase = newPhase;
    }

    public void UpdateMazePhase(MazePhase newPhase)
    {
        if ((int)newPhase > (int)mazePhase) mazePhase = newPhase;
    }

    public void UpdateLavaPhase(LavaPhase newPhase)
    {
        if ((int)newPhase > (int)lavaPhase) lavaPhase = newPhase;
    }

    public void UpdateGlyphPhase(GlyphPhase newPhase)
    {
        if ((int)newPhase > (int)glyphPhase) glyphPhase = newPhase;
    }

    // Custom Functions here as necessary
    public void OnFindPowerOutlet()
    {
        TorchPhase newPhase = TorchPhase.FindPowerOutlet;
        if ((int)newPhase > (int)torchPhase) torchPhase = newPhase;
    }

}
