namespace RPG_Assistant.Rules;

public static class SelectionGuard {
    public static List<T> ExcludeAlreadyChosen<T>(
        IEnumerable<T> candidates, IEnumerable<T> alreadyChosen) {
        var chosenSet = new HashSet<T>(alreadyChosen);
        return candidates.Where(item => !chosenSet.Contains(item)).ToList();
    }
}