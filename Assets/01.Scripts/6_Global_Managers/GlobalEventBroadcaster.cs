public static class GlobalEventBroadcaster {
    public static System.Action OnBossDefeated;
    public static void Trigger() => OnBossDefeated?.Invoke();
}
