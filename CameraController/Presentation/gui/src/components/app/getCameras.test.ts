import axios from 'axios';

import { getCamerasResponseSchema } from './CameraModel';
import getCameras from "./getCameras";

jest.mock('axios');

const mockedAxios = axios as jest.Mocked<typeof axios>;

afterEach(() => {
  jest.restoreAllMocks();
});

it('throws on API error', async () => {
  mockedAxios.get.mockRejectedValue('Some error');
  await expect(getCameras()).rejects.toThrow();
});

it('returns no cameras on empty response', async () => {
  mockedAxios.get.mockResolvedValue({data: []});
  await expect(getCameras()).resolves.toStrictEqual([]);
});

it("validates the API's response", async () => {
  mockedAxios.get.mockResolvedValue({data: [{foo: 'bar'}]});
  jest.spyOn(getCamerasResponseSchema, 'validate').mockRejectedValue('error');
  await expect(getCameras()).rejects.toThrow();
});

it('returns cameras from the API', async () => {
  mockedAxios.get.mockResolvedValue({data: [{model: 'foo'}]});
  jest.spyOn(getCamerasResponseSchema, 'validate').mockReturnValue(Promise.resolve({ cameras: [] }));

  const cameras = await getCameras();

  expect(cameras).toHaveLength(1);
  expect(cameras[0].model).toBe('foo');
});
