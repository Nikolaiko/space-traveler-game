using System.Collections;

public interface CoroutineScope
{
    void launch(IEnumerator routine);
}
