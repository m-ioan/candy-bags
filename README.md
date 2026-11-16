# candy-bags

A 2d platformer game created in 3 days for a game jam and built in Unity.

**[Play Candy Bags on itch.io](https://maridevio.itch.io/)**

Technical Highlights:
* **Event-Driven Collectibles (`Gems.cs` & `Collector.cs`):** Uses C# `Action<int>` events and a shared `IITEM` interface that triggers score updates and sound effects cleanly with a guard to prevent double-counting.
* **Raycast Enemy AI (`Enemy.cs`):** Uses downward, forward, and overhead `Physics2D.Raycast` checks alongside a jump cooldown to handle walls, gaps, and platform chasing.
* **Player Movement & Double Jump (`PlayerMovement.cs`):** Responsive 2D platformer controller featuring a mid-air double jump reset on ground contact.

