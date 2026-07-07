using RPG_Assistant.Enums;
namespace RPG_Assistant.Extensions;
public static class ExpertiseExtensions {
    public static AttributeType GetAssociatedAttribute(this ExpertiseType type) => type switch {
        ExpertiseType.Atletismo or
        ExpertiseType.Luta => AttributeType.Forca,

        ExpertiseType.Cavalgar or
        ExpertiseType.Acrobacia or
        ExpertiseType.Furtividade or
        ExpertiseType.Iniciativa or
        ExpertiseType.Ladinagem or
        ExpertiseType.Pilotagem or
        ExpertiseType.Reflexos => AttributeType.Destreza,

        ExpertiseType.Fortitude => AttributeType.Constituicao,

        ExpertiseType.Conhecimento or
        ExpertiseType.Guerra or
        ExpertiseType.Investigacao or
        ExpertiseType.Misticismo or
        ExpertiseType.Nobreza or
        ExpertiseType.Oficio or
        ExpertiseType.Pontaria => AttributeType.Inteligencia,

        ExpertiseType.Cura or
        ExpertiseType.Intuicao or
        ExpertiseType.Percepcao or
        ExpertiseType.Religiao or
        ExpertiseType.Sobrevivencia or
        ExpertiseType.Vontade => AttributeType.Sabedoria,

        ExpertiseType.Adestramento or
        ExpertiseType.Atuacao or
        ExpertiseType.Diplomacia or
        ExpertiseType.Enganacao or
        ExpertiseType.Intimidacao or
        ExpertiseType.Jogatina => AttributeType.Carisma,

        _ => AttributeType.Inteligencia
    };
}