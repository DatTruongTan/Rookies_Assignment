import {
    All,
    Nike,
    Adidas,
    AllLabel,
    NikeLabel,
    AdidasLabel,
} from './Brand/BrandConstants';

export const BrandTypeOptions = [
    { id: 1, label: NikeLabel, value: Nike },
    { id: 2, label: AdidasLabel, value: Adidas },
];

export const FilterBrandTypeOptions = [
    { id: 1, label: AllLabel, value: All },
    { id: 2, label: NikeLabel, value: Nike },
    { id: 3, label: AdidasLabel, value: Adidas },
];
