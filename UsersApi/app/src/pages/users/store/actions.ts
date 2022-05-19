import { PassesApiService } from 'src/api/users';
import * as consts from './consts';

const loadList = () => async (dispatch, getState) => {
    dispatch({ type: consts.LOAD_DATA_LOADING });
    const response = await new PassesApiService().list();
    dispatch({ type: consts.LOAD_DATA_SUCCESS, payload: response });
};


export default {
    loadList
};
