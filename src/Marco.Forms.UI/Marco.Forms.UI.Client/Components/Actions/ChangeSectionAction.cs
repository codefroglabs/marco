namespace Marco.Forms.UI.Client.Components.Actions;

public record ChangeSectionAction(string BlockId, string SectionId) : BlockAction(BlockId);
