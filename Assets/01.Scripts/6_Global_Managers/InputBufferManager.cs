public class InputBufferManager {
    public string Buffer { get; private set; } = "";
    public void SetBuffer(string k) => Buffer = k;
    public void Clear() => Buffer = "";
}
