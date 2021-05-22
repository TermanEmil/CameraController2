import axios from 'axios';
import CameraNotFoundError from 'components/common/exceptions/CameraNotFoundError';
import CommunicationError from 'components/common/exceptions/CommunicationError';
import UnexpectedResponseError from 'components/common/exceptions/UnexpectedResponseError';
import { saveAs } from 'file-saver';
import { StatusCodes } from 'http-status-codes';

import captureImage from './captureImage';

jest.mock('axios');
jest.mock('file-saver');

const mockedAxios = axios as jest.Mocked<typeof axios>;
const mockedSaveAs = saveAs as jest.Mock<typeof saveAs>;

beforeEach(() => {
  jest.clearAllMocks();
});

it('throws on API error', async () => {
  mockedAxios.request.mockRejectedValue('Some error');
  await expect(captureImage('123')).rejects.toThrowError(CommunicationError);
});

it("throws when the API cannot find the camera", async () => {
  mockedAxios.request.mockRejectedValue({ response: { status: StatusCodes.NOT_FOUND } });
  await expect(captureImage('123')).rejects.toThrowError(CameraNotFoundError);
});

it("throws when receives no data", async () => {
  mockedAxios.request.mockResolvedValue({ response: { data: undefined } });
  await expect(captureImage('123')).rejects.toThrowError(UnexpectedResponseError);
});

it("throws when receives no data type", async () => {
  mockedAxios.request.mockResolvedValue({ response: { data: {type: undefined } } });
  await expect(captureImage('123')).rejects.toThrowError(UnexpectedResponseError);
});

it("throws when receives an unrecognized data type", async () => {
  mockedAxios.request.mockResolvedValue({ data: { type: 'foo/bar' } });
  await expect(captureImage('123')).rejects.toThrowError(UnexpectedResponseError);
});

it("saves the image with the correct extension", async () => {
  mockedSaveAs.mockImplementation();
  mockedAxios.request.mockResolvedValue({ data: { type: 'image/png' } });

  await captureImage('123');

  expect(mockedSaveAs.mock.calls[0][1].endsWith('.png')).toBeTruthy();
});
