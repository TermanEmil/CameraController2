import axios from 'axios';

import getCameras from "./getCameras";

jest.mock('axios');
jest.mock('./urls');

const mockedAxios = axios as jest.Mocked<typeof axios>;

it('throws on API error', async () => {
  mockedAxios.get.mockRejectedValue('Some error');
  await expect(getCameras()).rejects.toThrow();
});

it('returns no cameras on empty response', async () => {
  mockedAxios.get.mockResolvedValue({data: []});
  await expect(getCameras()).resolves.toStrictEqual([]);
});

// TODO: try to mock the validation
it("validates the API's response", async () => {
  mockedAxios.get.mockResolvedValue({data: [{foo: 'bar'}]});
  await expect(getCameras()).rejects.toThrow();
});

it('returns cameras from the API', async () => {
  mockedAxios.get.mockResolvedValue({data: [{model: 'foo', port: 'bar'}]});
  await expect(getCameras()).resolves.toHaveLength(1);
});
