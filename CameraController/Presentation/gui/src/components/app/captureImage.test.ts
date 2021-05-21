import axios from 'axios';
import { StatusCodes } from 'http-status-codes';

import captureImage from './captureImage';

jest.mock('axios');

const mockedAxios = axios as jest.Mocked<typeof axios>;

afterEach(() => {
  jest.restoreAllMocks();
});

it('throws on API error', async () => {
  mockedAxios.request.mockRejectedValue('Some error');
  await expect(captureImage('123')).rejects.toThrowError('Failed to communicate with the API');
});

it("throws when the API cannot find a camera on the specified port", async () => {
  mockedAxios.request.mockRejectedValue({ response: { status: StatusCodes.NOT_FOUND } });
  await expect(captureImage('123')).rejects.toThrowError('The API responded with Not Found');
});
