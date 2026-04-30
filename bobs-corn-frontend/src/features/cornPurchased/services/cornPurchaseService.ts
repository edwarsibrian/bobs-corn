import axios from 'axios';
import apiClient from '../../../services/apiClient';
import { getClientId } from '../../../utils/clientId';
import type { BuyCornResult } from '../types/cornPurchase';

export async function buyCorn(): Promise<BuyCornResult> {
    try {
        const response = await apiClient.post<BuyCornResult>(
            '/corn/buy',
            {},
            {
                headers: {
                    'X-Client-ID': getClientId(),
                },
            }
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError<BuyCornResult>(error) && error.response) {
            return error.response.data;
        }

        return {
            success: false,
            totalCornBought: 0,
            message: "Could not connect to the API.",
        };
    }
}