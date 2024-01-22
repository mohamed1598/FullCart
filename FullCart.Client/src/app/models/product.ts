export interface IProduct{
    id: number;
    name: string;
    pictureUrl: string;
    picture:File|null;
    removeImage:boolean;
    description: string;
    price: number;
    brandId: number;
    categoryId: number;
}