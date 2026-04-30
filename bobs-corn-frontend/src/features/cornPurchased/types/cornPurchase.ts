export interface BuyCornResult {
    success: boolean;
    totalCornBought: number;
    message: string;
    retryAfterSeconds?: number;
}