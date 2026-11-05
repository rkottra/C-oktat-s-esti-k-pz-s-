namespace Edzoterem;

public class Terem
{
    private readonly List<Tag> tagok = new();
    private readonly List<Belepes> belepesek = new();

    public void UjTag(Tag tag) => tagok.Add(tag);

    public bool Belep(int tagSzam, DateOnly nap)
    {
        Tag? tag = tagok.FirstOrDefault(t => t.TagSzam == tagSzam);
        if (tag?.Berlet == null || !tag.Berlet.Ervenyes(nap))
            return false;

        tag.Berlet.Belepes(nap);
        belepesek.Add(new Belepes(tagSzam, nap));
        return true;
    }

    public int NapiBelepesek(DateOnly nap) => belepesek.Count(b => b.Nap == nap);
}
